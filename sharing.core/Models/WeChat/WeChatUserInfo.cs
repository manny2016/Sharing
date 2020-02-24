

namespace Sharing.WeChat.Models {
	using Newtonsoft.Json;
	using Sharing.Core;
	public class WeChatUserInfo : IEntityWithTimestamp {

		[JsonProperty("subscribe")]
		public bool Subscribe { get; set; }

		[JsonProperty("openid")]
		public string OpenId { get; set; }

		[JsonProperty("nickName")]
		public string NickName { get; set; }


		[JsonProperty("gender")]
		public string Gender { get; set; }


		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("province")]
		public string Province { get; set; }


		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("avatarUrl")]
		public string AvatarUrl { get; set; }

		[JsonProperty("unionId")]
		public string UnionId { get; set; }

		[JsonProperty("watermark")]
		public Watermark watermark { get; set; }
		[JsonIgnore]
		public long Timestamp { get; set; }
	}
	public class Watermark {
		[JsonProperty("appid")]
		public string AppId { get; set; }

		[JsonProperty("timestamp")]
		public string TimeStamp { get; set; }
	}
}
