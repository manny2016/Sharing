

namespace Sharing.Core.CMQ
{
    using Newtonsoft.Json;
    public class QueueMeta : CMQBaseMessage
    {
        public QueueMeta()
        {
            this.MaxMsgHeapNum = -1;
            this.PollingWaitSeconds = DEFAULT_POLLING_WAIT_SECONDS;
            this.VisibilityTimeout = DEFAULT_VISIBILITY_TIMEOUT;
            this.MaxMsgSize = DEFAULT_MAX_MSG_SIZE;
            this.MsgRetentionSeconds = DEFAULT_MSG_RETENTION_SECONDS;
            this.CreateTime = -1;
            this.LastModifyTime = -1;
            this.ActiveMsgNum = -1;

        }
        //缺省消息接收长轮询等待时间
        public static readonly int DEFAULT_POLLING_WAIT_SECONDS = 0;
        //缺省消息可见性超时
        public static readonly int DEFAULT_VISIBILITY_TIMEOUT = 30;
        //缺省消息最大长度，单位字节
        public static readonly int DEFAULT_MAX_MSG_SIZE = 65536;
        //缺省消息保留周期，单位秒
        public static readonly int DEFAULT_MSG_RETENTION_SECONDS = 345600;

        /// <summary>
        ///  最大堆积消息数 
        /// </summary>
        [JsonProperty("maxMsgHeapNum")]
        public int MaxMsgHeapNum { get; set; }

        /// <summary>
        /// 消息接收长轮询等待时间
        /// </summary>
        [JsonProperty("pollingWaitSeconds")]
        public int PollingWaitSeconds { get; set; }

        /// <summary>
        /// /** 消息可见性超时 */
        /// </summary>
        [JsonProperty("visibilityTimeout")]
        public int VisibilityTimeout { get; set; }

        /// <summary>
        /// /** 消息最大长度 */
        /// </summary>
        [JsonProperty("maxMsgSize")]
        public int MaxMsgSize { get; set; }

        /// <summary>
        /// /** 消息保留周期 */
        /// </summary>
        [JsonProperty("msgRetentionSeconds")]
        public int MsgRetentionSeconds { get; set; }

        /// <summary>
        /// /** 队列创建时间 */
        /// </summary>
        [JsonProperty("createTime")]
        public int CreateTime { get; set; }
        /// <summary>
        /// /** 队列属性最后修改时间 */
        /// </summary>
        [JsonProperty("lastModifyTime")]
        public int LastModifyTime { get; set; }
        /// <summary>
        /// /** 队列处于Active状态的消息总数 */
        /// </summary>
        [JsonProperty("activeMsgNum")]
        public int ActiveMsgNum { get; set; }
        /// <summary>
        /// /** 队列处于Inactive状态的消息总数 */
        /// </summary>
        [JsonProperty("inactiveMsgNum ")]
        public int InactiveMsgNum { get; set; }
        /// <summary>
        /// /** 已删除的消息，但还在回溯保留时间内的消息数量 */
        /// </summary>
        [JsonProperty("rewindmsgNum")]
        public int RewindmsgNum { get; set; }
        /// <summary>
        /// /** 消息最小未消费时间 */
        /// </summary>
        [JsonProperty("minMsgTime")]
        public int MinMsgTime { get; set; }
        /// <summary>
        /// /** 延时消息数量 */
        /// </summary>
        [JsonProperty("delayMsgNum")]
        public int DelayMsgNum { get; set; }
    }
}
