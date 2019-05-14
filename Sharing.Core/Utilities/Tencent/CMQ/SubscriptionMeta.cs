
namespace Sharing.Core.CMQ
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class SubscriptionMeta : CMQBaseMessage
    {
        public static readonly string notifyStrategyDefault = "BACKOFF_RETRY";
        public static readonly string notifyContentFormatDefault = "JSON";
        public static readonly string protocalDefault = "Queue";

        /// <summary>
        /// //订阅的终端地址
        /// </summary>
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// //订阅的协议
        /// </summary>
        [JsonProperty("protocal")]
        public string Protocal { get; set; }

        /// <summary>
        /// //推送消息出现错误时的重试策略
        /// </summary>
        [JsonProperty("notifyStrategy")]
        public string NotifyStrategy { get; set; }
        /// <summary>
        /// //向 Endpoint 推送的消息内容格式
        /// </summary>
        [JsonProperty("notifyContentFormat")]
        public string NotifyContentFormat { get; set; }
        //描述了该订阅中消息过滤的标签列表（仅标签一致的消息才会被推送）

        [JsonProperty("filterTag")]
        public List<string> FilterTag { get; set; }

        /// <summary>
        /// Subscription 的创建时间，从 1970-1-1 00:00:00 到现在的秒值
        /// </summary>
        [JsonProperty("createTime")]
        public int CreateTime { get; set; }
        /// <summary>
        /// //修改 Subscription 属性信息最近时间，从 1970-1-1 00:00:00 到现在的秒值
        /// </summary>
        [JsonProperty("lastModifyTime")]
        public int LastModifyTime { get; set; }

        /// <summary>
        /// //该订阅待投递的消息数
        /// </summary>
        [JsonProperty("msgCount")]
        public int MsgCount { get; set; }

        [JsonProperty("bindingKey")]
        public List<string> BindingKey { get; set; }


        /**
         * subscription meta class .
         *
         */
        public SubscriptionMeta()
        {
            this.Endpoint = "";
            this.Protocal = protocalDefault;
            this.NotifyStrategy = notifyStrategyDefault;
            this.NotifyContentFormat = notifyContentFormatDefault;
            this.FilterTag = null;
            this.CreateTime = 0;
            this.LastModifyTime = 0;
            this.MsgCount = 0;
            this.BindingKey = null;
        }
    }
}
