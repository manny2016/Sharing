


namespace Sharing.Core.CMQ
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;

    public class TencentCMQClient<T>
        where T : ICMQMessageHandle
    {

        private const int Polltime = 10;//轮询时间间隔
        private CancellationTokenSource Cancellation = new CancellationTokenSource();
        public TencentCMQClient(ClientMeta meta)
        {
            this.Meta = meta;
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
        }
        public void Initialize()
        {
            this.CreateTopic(this.Meta.TopicName);
            this.CreateQueue(this.Meta.QueueName);
            this.CreateSubscribe(this.Meta.TopicName, this.Meta.QueueName, this.Meta.SubscriptionName);
        }
        public ClientMeta Meta { get; private set; }
        /// <summary>
        /// 创建队列，如果存在则直接返回
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public Queue CreateQueue(string queueName)
        {
            var queue = new Queue(queueName, this.Meta);
            var meta = queue.GetQueueAttributes();
            if (meta == null)
            {
                this.CreateQueue(queueName, new QueueMeta());
            }
            return queue;
        }

        /// <summary>
        /// 创建 Topic 如果存在则直接返回
        /// </summary>
        /// <param name="topicName"></param>
        /// <returns></returns>
        public Topic CreateTopic(string topicName)
        {
            var topic = new Topic(topicName, this.Meta);
            var meta = topic.GetTopicAttributes();
            if (meta == null)
            {
                this.CreateTopic(topicName, 65536);
            }
            return topic;
        }
        public Subscription CreateSubscribe(string topicName, string queueName, string subscriptionName)
        {
            var subscription = new Subscription(topicName, subscriptionName, this.Meta);
            var meta = subscription.GetSubscriptionAttributes();
            if (meta == null)
            {
                CreateSubscribe(topicName, subscriptionName, queueName, new SubscriptionMeta()
                {
                    FilterTag = new List<string>() { "OnlineOrder" },
                    Endpoint = queueName,
                    BindingKey = new List<string>() { "OnlineOrder" }
                });
            }
            return subscription;
        }

        public async Task Monitor(string[] queueNames, Action<T> notify)
        {
            Parallel.ForEach(queueNames, (name) =>
            {
                while (this.Cancellation.IsCancellationRequested == false)
                {
                    int offset = 10;
                    var queue = new Queue(name, this.Meta);
                    var message = queue.ReceiveMessage(offset);
                    if (message.Code == 0)
                    {
                        var model = message.MsgBody.DeserializeToObject<T>();
                        model.ReceiptHandle = message.ReceiptHandle;
                        notify(model);
                    }

                    for (var a = 0; ((this.Cancellation.IsCancellationRequested == false) && (a < offset)); a++)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });
        }
        public void Abort()
        {
            this.Cancellation.Cancel();
        }

        public async void Monitor(Action<T> notify)
        {
           await this.Monitor(new string[] { this.Meta.QueueName }, notify);
        }
        public void Push(T[] models)
        {
            var topic = new Topic(this.Meta.TopicName, this.Meta);
            foreach (var model in models)
            {
                topic.PublishMessage(model.SerializeToJson(), new List<string>() { "OnlineOrder" }, "OnlineOrder");
            }
        }
        public void DeleteMessage(ICMQMessageHandle handle)
        {
            var queue = new Queue(this.Meta.QueueName, this.Meta);
            queue.DeleteMessage(handle.ReceiptHandle);
        }
        private void CreateQueue(string queueName, QueueMeta meta)
        {
            var parameters = this.Meta.CreateGeneralParameters(CMQConstant.CreateQueue);
            if (queueName == "")
                throw new ClientException("Invalid parameter: queueName is empty");
            else
                parameters.Add("queueName", queueName);

            if (meta.MaxMsgHeapNum > 0)
                parameters.Add("maxMsgHeapNum", Convert.ToString(meta.MaxMsgHeapNum));
            if (meta.PollingWaitSeconds > 0)
                parameters.Add("pollingWaitSeconds", Convert.ToString(meta.PollingWaitSeconds));
            if (meta.VisibilityTimeout > 0)
                parameters.Add("visibilityTimeout", Convert.ToString(meta.VisibilityTimeout));
            if (meta.MaxMsgSize > 0)
                parameters.Add("maxMsgSize", Convert.ToString(meta.MaxMsgSize));
            if (meta.MsgRetentionSeconds > 0)
                parameters.Add("msgRetentionSeconds", Convert.ToString(meta.MsgRetentionSeconds));

            var signature = Sign.Signature(parameters, this.Meta.Endpoint.Queue, this.Meta.Path, this.Meta.SecretKey, "POST", this.Meta.SignMethod);
            parameters.Add("Signature", signature);
            var result = string.Concat(this.Meta.Endpoint.Queue, this.Meta.Path)
                .GetUriJsonContent<GeneralCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
            return;
        }

        public void CreateTopic(string topicName, int maxMsgSize, int filterType = 1)
        {
            Guard.ArgumentNotNullOrEmpty(topicName, "topicName");

            var parameters = this.Meta.CreateGeneralParameters(CMQConstant.CreateTopic);
            parameters.Add("topicName", topicName);
            parameters.Add("filterType", Convert.ToString(filterType));
            if (maxMsgSize < 1 || maxMsgSize > 65536)
                throw new ClientException("Invalid paramter: maxMsgSize > 65536 or maxMsgSize < 1 ");

            var signature = Sign.Signature(
                parameters,
                this.Meta.Endpoint.Topic,
                this.Meta.Path,
                this.Meta.SecretKey,
                "POST",
                this.Meta.SignMethod);


            parameters.Add("Signature", signature);
            var result = string.Concat(this.Meta.Endpoint.Topic, this.Meta.Path)
                .GetUriJsonContent<GeneralCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
        }
        public void CreateSubscribe(
            string topicName,
            string subscriptionName,
            string queueName,
            SubscriptionMeta meta)
        {
            if (meta.FilterTag != null && meta.FilterTag.Count > 5)
                throw new ClientException("Invalid parameter:Tag number > 5");
            var parameters = this.Meta.CreateGeneralParameters(CMQConstant.Subscribe);
            if (topicName == "")
                throw new ClientException("Invalid parameter: topicName is empty");
            else
                parameters.Add("topicName", topicName);

            if (subscriptionName == "")
                throw new ClientException("Invalid parameter: subscriptionName is empty");
            else
                parameters.Add("subscriptionName", subscriptionName);
            if (meta.Endpoint == "")
                throw new ClientException("Invalid parameter: endpoint is empty");
            else
                parameters.Add("endpoint", meta.Endpoint);

            if (meta.Protocal == "")
                throw new ClientException("Invalid parameter: protocol is empty");
            else
                parameters.Add("protocol", meta.Protocal);

            if (meta.NotifyStrategy == "")
                throw new ClientException("Invalid parameter: notifyStrategy is empty");
            else
                parameters.Add("notifyStrategy", meta.NotifyStrategy);
            if (meta.NotifyContentFormat == "")
                throw new ClientException("Invalid parameter: notifyContentFormat is empty");
            else
                parameters.Add("notifyContentFormat", meta.NotifyContentFormat);

            if (meta.FilterTag != null)
            {
                for (int i = 0; i < meta.FilterTag.Count; ++i)
                    parameters.Add("filterTag." + Convert.ToString(i), meta.FilterTag[i]);
            }

            if (meta.BindingKey != null)
            {
                for (int i = 0; i < meta.BindingKey.Count; ++i)
                    parameters.Add("bindingKey." + Convert.ToString(i), meta.BindingKey[i]);
            }

            var signature = Sign.Signature(
             parameters,
             this.Meta.Endpoint.Topic,
             this.Meta.Path,
             this.Meta.SecretKey,
             "POST",
             this.Meta.SignMethod);

            parameters.Add("Signature", signature);
            var result = string.Concat(this.Meta.Endpoint.Topic, this.Meta.Path)
            .GetUriJsonContent<GeneralCMQBaseMessage>((http) =>
            {
                return http.Wrap(parameters.CreateRequestBody());
            });
            result.IfHasErrorThrowException();
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

    }
}
