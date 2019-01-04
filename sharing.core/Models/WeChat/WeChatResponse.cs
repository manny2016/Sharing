


namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public abstract class WeChatResponse
    {
        [JsonProperty("errcode")]
        public virtual int ErrCode { get; set; }



        [JsonProperty("errmsg")]
        public virtual string ErrMsg { get; set; }


        [JsonIgnore]
        public virtual bool HasError
        {
            get
            {
                return ErrCode != 0;
            }
        }
    }
}
