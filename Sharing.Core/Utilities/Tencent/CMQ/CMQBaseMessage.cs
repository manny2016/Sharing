

namespace Sharing.Core.CMQ
{
    using Newtonsoft.Json;
    public abstract class CMQBaseMessage
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        
        public void IfHasErrorThrowException()
        {
            if (this.Code != 0)
                throw new ServerException(this.Code, this.Message, this.RequestId);
        }
    }
    public class GeneralCMQBaseMessage: CMQBaseMessage
    {
        
    }
    public class GeneralMessageCMQBaseMessage : CMQBaseMessage
    {
        [JsonProperty("msgId")]
        public string MsgId { get; set; }
    }
    public class GeneralQueueCMQBaseMessage: CMQBaseMessage
    {
        [JsonProperty("queueId")]
        public string QueueId { get; set; }
    }
    public class SubscriptionListCMQBaseMessage : CMQBaseMessage
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("subscriptionList")]
        public Subscriper[] List { get; set; }
    }
    public class Subscriper
    {
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("subscriptionName")]
        public string SubscriptionName { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
    }

}
