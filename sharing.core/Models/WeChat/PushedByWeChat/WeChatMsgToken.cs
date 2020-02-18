

using Sharing.Core;

namespace Sharing.WeChat.Models {
	public class WeChatMsgToken : IWeChatMsgToken {
		public WeChatMsgToken(
			string signature,
			string timestamp,
			string nonce,
			string reqmsg,
			string[] appids) {
			this.Signature = signature;
			this.TimeStamp = timestamp;
			this.Nonce = nonce;
			this.ReqMsg = reqmsg;
			this.BizMsgToken = IoC.GetService<WeChatConstant>().WxBizMsgToken; //WeChatConstant.WxBizMsgToken;
			this.EncodingAESKey = IoC.GetService<WeChatConstant>().EncodingAESKey;// WeChatConstant.EncodingAESKey;
			this.AppIds = appids;
		}

		public string Signature { get; private set; }

		public string TimeStamp { get; private set; }

		public string Nonce { get; private set; }

		public string ReqMsg { get; private set; }

		public string BizMsgToken { get; private set; }

		public string EncodingAESKey { get; private set; }

		public string[] AppIds { get; private set; }
		public string CurrentAppId { get; set; }
	}
}

