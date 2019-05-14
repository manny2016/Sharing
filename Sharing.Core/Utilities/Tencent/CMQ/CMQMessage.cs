


namespace Sharing.Core.CMQ
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class CMQMessage : CMQBaseMessage
    {

        /// <summary>
        /// /** 每次消费唯一的消息句柄，用于删除等操作 */
        /// </summary>
        [JsonProperty("receiptHandle")]
        public string ReceiptHandle { get; set; }
        /// <summary>
        /// /** 消息体 */
        /// </summary>
        [JsonProperty("msgBody")]
        public string MsgBody { get; set; }

        /// <summary>
        /// /** 消息发送到队列的时间，从 1970年1月1日 00:00:00 000 开始的毫秒数 */
        /// </summary>
        [JsonProperty("enqueueTime")]
        public long EnqueueTime;

        /// <summary>
        /// /** 消息下次可见的时间，从 1970年1月1日 00:00:00 000 开始的毫秒数 */
        /// </summary>
        [JsonProperty("nextVisibleTime")]
        public long NextVisibleTime;

        /// <summary>
        /// /** 消息第一次出队列的时间，从 1970年1月1日 00:00:00 000 开始的毫秒数 */
        /// </summary>
        [JsonProperty("firstDequeueTime")]
        public long FirstDequeueTime { get; set; }

        /// <summary>
        /// /** 出队列次数 */
        /// </summary>
        [JsonProperty("dequeueCount")]
        public int DequeueCount { get; set; }

        /// <summary>
        /// 消息 tag
        /// </summary>
        [JsonProperty("msgTag")]
        public string[] MsgTag { get; set; }
    }

 
}
