using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Sharing.WeChat.Models
{
    /// <summary>
    /// 发放现金红包返回结果实体类
    /// </summary>
    [XmlRoot("xml")]
    public class RedpackResponse
    {
        /// <summary>
        /// 返回状态码	return_code	是	SUCCESS	String(16)	        SUCCESS/FAIL        此字段是通信标识，非红包发放结果标识，红包发放是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息	return_msg	否	签名失败	String(128)	        返回信息，如非空，为错误原因        签名失败 参数格式校验错误
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string mch_billno { get; set; }
        public string mch_id { get; set; }
        public string wxappid { get; set; }
        public string re_openid { get; set; }
        public string total_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string send_listid { get; set; }     
    }
}
