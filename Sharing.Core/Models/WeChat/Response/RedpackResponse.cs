


namespace Sharing.WeChat.Models
{
	using System.Xml;
	using System.Xml.Serialization;
	/// <summary>
	/// 发放现金红包返回结果实体类
	/// </summary>
	[XmlRoot("xml")]
    public class RedpackResponse
    {
		/// <summary>
		/// 返回状态码	return_code	是	SUCCESS	String(16)	        SUCCESS/FAIL        此字段是通信标识，非红包发放结果标识，红包发放是否成功需要查看result_code来判断
		/// </summary>
		private string return_code;
		[XmlElement("return_code")]
		public XmlCDataSection ReturnCode {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(return_code);
			}
			set {
				return_code = value.Value;
			}
		}


		/// <summary>
		/// 返回信息	return_msg	否	签名失败	String(128)	        返回信息，如非空，为错误原因        签名失败 参数格式校验错误
		/// </summary>
		private string returnMsg;
		[XmlElement("return_msg")]
		public XmlCDataSection ReturnMessage {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(returnMsg);
			}
			set {
				returnMsg = value.Value;
			}
		}
		private string resultCode;
		[XmlElement("result_code")]
		public XmlCDataSection ResultCode {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(resultCode);
			}
			set {
				resultCode = value.Value;
			}
		}

		private string errCode;
		[XmlElement("err_code")]
		public XmlCDataSection ErrorCode {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(errCode);
			}
			set {
				errCode = value.Value;
			}
		}
		private string errCodeDes;
		[XmlElement("err_code_des")]
		public XmlCDataSection ErrorCodeDescription {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(errCodeDes);
			}
			set {
				errCodeDes = value.Value;
			}
		}
		/// <summary>
		/// 商户订单号
		/// </summary>
		private string mchBillNo;
		[XmlElement("mch_billno")]
		public XmlCDataSection MchBillNo {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(mchBillNo);
			}
			set {
				mchBillNo = value.Value;
			}
		}
		private string mchid;
		[XmlElement("mch_id")]
		public XmlCDataSection MchId {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(mchid);
			}
			set {
				mchid = value.Value;
			}
		}
		private string wxappid;
		[XmlElement("wxappid")]
		public XmlCDataSection WxAppId {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(wxappid);
			}
			set {
				wxappid = value.Value;
			}
		}
		private string reopenid;
		[XmlElement("re_openid")]
		public XmlCDataSection ReOpenId {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(reopenid);
			}
			set {
				reopenid = value.Value;
			}
		}
		private string total_amount;
		[XmlElement("total_amount")]
		public string TotalAmount {
			get;set;
		}
		private string sendListId;
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("send_listid")]
		public XmlCDataSection SendListId {
			get {
				XmlDocument doc = new XmlDocument();
				return doc.CreateCDataSection(sendListId);
			}
			set {
				sendListId = value.Value;
			}
		}
	}
}
