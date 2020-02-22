using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.WeChat.Models {
	using Microsoft.Extensions.Configuration;
	using Sharing.Core;
	public class WxMsgToken : IWeChatMsgToken {

		public WxMsgToken(
			string signature,
			string timestamp,
			string nonce,
			string reqmsg,
			string wxAppOriginalId,IConfiguration configuration) {
			this.Signature = signature;
			this.TimeStamp = timestamp;
			this.Nonce = nonce;
			this.ReqMsg = reqmsg;
			this.BizMsgToken = configuration.GetWeChatConstant().WxBizMsgToken; //WeChatConstant.WxBizMsgToken;
			this.EncodingAESKey = configuration.GetWeChatConstant().EncodingAESKey; //WeChatConstant.EncodingAESKey;
			this.OriginalId = wxAppOriginalId;
		}

		public string Signature { get; private set; }

		public string TimeStamp { get; private set; }

		public string Nonce { get; private set; }

		public string ReqMsg { get; private set; }

		public string OriginalId { get; private set; }

		public string BizMsgToken { get; private set; }

		public string EncodingAESKey { get; private set; }
	}
}
