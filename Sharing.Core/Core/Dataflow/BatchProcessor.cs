

namespace Sharing.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks.Dataflow;

    public class BatchProcessor<T>
        where T : IEntityWithTimestamp
    {
        protected Timer TriggerBatchTimer;
        protected BatchBlock<T> Collector;
        protected ActionBlock<IEnumerable<T>> Processor;

        public Action<IEnumerable<T>> ProcessAction { get; private set; }
        public int BatchSize { get; private set; }
        /// <summary>
        /// The interval (unit: seconds) of trigger the submission of data from the collector to the processor.
        /// (&lt; 0) is never trigger (only util the volume is greater than Batch Size setting).
        /// </summary>
        public int TriggerInterval { get; private set; }
        public int ProcessParallelismDegree { get; private set; }
        public bool IsWaitingOnRemainingEntitiesProcessingWhenCancellationRequested { get; private set; }

        public BatchProcessor(
            Action<IEnumerable<T>> action,
            int size, int interval,
            int degree,
            bool isWaiting)
        {
            this.ProcessAction = action;
            this.BatchSize = size;
            this.TriggerInterval = interval;
            this.ProcessParallelismDegree = degree;
            this.IsWaitingOnRemainingEntitiesProcessingWhenCancellationRequested = isWaiting;
        }

        public void Initialize(CancellationToken token)
        {
            if (this.IsWaitingOnRemainingEntitiesProcessingWhenCancellationRequested)
            {
                this.Collector = new BatchBlock<T>(
                    this.BatchSize);
                this.Processor = new ActionBlock<IEnumerable<T>>(
                    this.ProcessAction,
                    new ExecutionDataflowBlockOptions
                    {
                        MaxDegreeOfParallelism = this.ProcessParallelismDegree
                    });
            }
            else
            {
                this.Collector = new BatchBlock<T>(
                    this.BatchSize,
                    new GroupingDataflowBlockOptions { CancellationToken = token });
                this.Processor = new ActionBlock<IEnumerable<T>>(
                    this.ProcessAction,
                    new ExecutionDataflowBlockOptions
                    {
                        CancellationToken = token,
                        MaxDegreeOfParallelism = this.ProcessParallelismDegree
                    });
            }
            this.Collector.LinkTo(this.Processor);
            this.TriggerBatchTimer = new Timer(
                state => this.Collector.TriggerBatch(),
                null,
                1000 * this.TriggerInterval, 1000 * TriggerInterval);
        }

        public void Pass(T data)
        {
            this.Collector.Post(data);
        }
        public void TriggerBatch()
        {
            this.Collector.TriggerBatch();
        }

        public void Complete()
        {
            this.Collector.Complete();
            if (this.Collector.Completion != null)
            {
                this.Collector.Completion.Wait();
            }
            //Thread.Sleep(100); //// prevent complete the action block too fast, before the remaining data in batch block have not transformed to action block.
            this.Processor.Complete();
            if (this.Processor.Completion != null)
            {
                this.Processor.Completion.Wait();
            }
        }
    }
}
