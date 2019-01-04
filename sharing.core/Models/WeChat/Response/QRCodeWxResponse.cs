
namespace Sharing.WeChat.Models
{
    public class QRCodeWxResponse:WeChatResponse
    {
        //{ "errcode":0,"errmsg":"ok","ticket":"gQGX7zwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAyMkZNbVVqeHlkb2oxU1dfRWhxNGwAAgSyeOhaAwQIBwAA","expire_seconds":1800,"url":"http:\/\/weixin.qq.com\/q\/022FMmUjxydoj1SW_Ehq4l","show_qrcode_url":"https:\/\/mp.weixin.qq.com\/cgi-bin\/showqrcode?ticket=gQGX7zwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAyMkZNbVVqeHlkb2oxU1dfRWhxNGwAAgSyeOhaAwQIBwAA"}
        [Newtonsoft.Json.JsonProperty("ticket")]
        public virtual string Ticket { get; set; }

        [Newtonsoft.Json.JsonProperty("expire_seconds")]
        public virtual int ExpireSeconds { get; set; }

        [Newtonsoft.Json.JsonProperty("url")]
        public virtual string Url { get; set; }

        [Newtonsoft.Json.JsonProperty("show_qrcode_url")]
        public virtual string ShowQRCodeUrl { get; set; }
    }
}