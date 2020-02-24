
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    using System;
    using System.Threading;
    using Microsoft.Extensions.Logging;
    public class WorkItemWithDataflow<T, E>
        : ASfPWorkItem<T>
        where T : ProcessState<E>
        where E : IEntityWithTimestamp
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(WorkItemWithDataflow<T, E>));
        protected BatchProcessor<E> Processor;
        public WorkItemWithDataflow(
            T state) : base(state)
        {

        }
		 
        protected override void Run(CancellationToken token)
        {
            this.InitializeBlocks(token);
            var service = this.WorkItemState.Setting.GenerateProcessService();
            service.Process(this.Pass, token);
        }
        protected virtual void Pass(E data)
        {
            this.Processor.Pass(data);
            this.WorkItemState.LastUpdatedTime = data.Timestamp;
            this.WorkItemState.SKUWorkItemCount++;
        }
        protected virtual void Process(IEnumerable<E> data)
        {
            using (var service = this.WorkItemState.GenerateProcessingResultService())
            {
                service.Save(data);
                this.WorkItemState.Update();
            }
        }

        private void OnProcess(IEnumerable<E> data)
        {
            if (data != null)
            {
                this.Process(data);
            }
        }
        protected virtual void InitializeBlocks(CancellationToken token)
        {
            this.Processor = new BatchProcessor<E>(
                (data) => this.OnProcess(data),
                this.WorkItemState.BatchSize,
                60,
                this.WorkItemState.MaxDegree,
                this.WorkItemState.IsWaitingOnRemainingEntitiesProcessingWhenCancellationRequested);
            this.Processor.Initialize(token);
        }
    }
}
