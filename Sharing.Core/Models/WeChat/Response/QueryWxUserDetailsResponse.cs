

namespace Sharing.WeChat.Models { 
	public class QueryWxUserDetailsResponse {
		public string NextOpenId { get;set;}
		public WeChatUserInfo[] WeChatUserInfos { get;set;}
	}
}
