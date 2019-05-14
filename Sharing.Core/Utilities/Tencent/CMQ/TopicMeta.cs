
namespace Sharing.Core.CMQ
{
    using Newtonsoft.Json;
    public class TopicMeta : CMQBaseMessage
    {
        /// <summary>
        /// // 当前该主题的消息堆积数
        /// </summary>
        [JsonProperty("msgCount")]
        public int MsgCount { get; set; }

        /// <summary>
        /// 消息最大长度，取值范围1024-65536 Byte（即1-64K），默认65536
        /// </summary>
        [JsonProperty("maxMsgSize")]
        public int MaxMsgSize { get; set; }

        /// <summary>
        /// //消息在主题中最长存活时间，从发送到该主题开始经过此参数指定的时间后，
        //不论消息是否被成功推送给用户都将被删除，单位为秒。固定为一天，该属性不能修改。
        /// </summary>
        [JsonProperty("msgRetentionSeconds")]
        public int MsgRetentionSeconds { get; set; }

        /// <summary>
        /// //创建时间 unix 时间戳
        /// </summary>
        [JsonProperty("createTime")]
        public int CreateTime;
        /// <summary>
        /// //最近修改属性信息最近时间 unix 时间戳
        /// </summary>
        [JsonProperty("lastModifyTime")]
        public int LastModifyTime { get; set; }

        //用于指定主题的消息匹配策略：
        //filterType =1或为空， 表示该主题下所有订阅使用 filterTag 标签过滤；
        //filterType =2 表示用户使用 bindingKey 过滤。
        //该参数设定之后不可更改。
        [JsonProperty("filterType")]
        public int FilterType { get; set; }

        public TopicMeta()
        {
            this.MaxMsgSize = 65536;
            this.MsgCount = 0;
            this.MsgRetentionSeconds = 86400;
            this.CreateTime = 0;
            this.LastModifyTime = 0;
            this.FilterType = 1;
        }

    }
}
