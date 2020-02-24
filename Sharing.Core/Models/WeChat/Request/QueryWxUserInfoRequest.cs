using System;
using System.Collections.Generic;
using System.Text;
using Sharing.Core;
using Sharing.Core.Models;

namespace Sharing.WeChat.Models {
	public class QueryWxUserInfoRequest {
		public WxApp WxApp { get; set; }
		public string NextOpenId { get; set; }
	}
}
