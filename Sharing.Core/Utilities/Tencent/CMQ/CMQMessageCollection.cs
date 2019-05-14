
namespace Sharing.Core.CMQ
{
    using Newtonsoft.Json;
    public class CMQMessageCollection : CMQBaseMessage
    {
        [JsonProperty("msgInfoList")]
        public CMQMessage[] MsgInfoList { get; set; }
    }
    public class CMQBatchMessage : CMQBaseMessage
    {
        [JsonProperty("msgList")]
        public string[] MsgList { get; set; }
    }
}
