using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Tests.Models {
	public class QRCode {
		public string ContentType { get; set; }
		public int ErrCode { get; set; }
		public string ErrMsg { get; set; }
		public byte[] Buffer { get; set; }

	}
}
