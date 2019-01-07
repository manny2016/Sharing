

namespace Sharing.WeChat.Models
{
    using System.ComponentModel;
/*
* 微信支付文档
* https://pay.weixin.qq.com/wiki/doc/api/H5.php?chapter=9_20&index=1
*/
    using System.Xml.Serialization;
    [XmlRoot("xml")]

    public class WxPayData
    {
        public const string SIGN_TYPE_MD5 = "MD5";
        public const string SIGN_TYPE_HMAC_SHA256 = "HMAC-SHA256";
        /// <summary>
        /// 公众账号ID
        /// </summary>
        [XmlElement("appid")]
        public string AppId { get; set; }

        [XmlElement("attach")]
        public string Attach { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }
        /// <summary>
        /// 设备号 
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }
        /// <summary>
        /// 签名类型
        /// </summary>
        [XmlElement("sign_type")]
        public string SignType { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [XmlElement("body")]
        public string Body { get; set; }

        /// <summary>
        /// 商品详情（暂未上线【来着微信】）
        /// </summary>
        [XmlElement("detail")]
        public string Detail { get; set; }


        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        [DefaultValue("CNY")]
        [XmlElement("fee_type")]
        public string Fee_type { get; set; }
        /// <summary>
        /// 订单总金额 （以分问单位）
        /// </summary>
        [XmlElement("total_fee")]
        public int? Totalfee { get; set; }
        /// <summary>
        /// 终端IP 必须传正确的用户端IP, IP 获取方法  https://pay.weixin.qq.com/wiki/doc/api/H5.php?chapter=15_5
        /// </summary>
        [XmlElement("spbill_create_ip")]
        public string SpbillCreateIp { get; set; }

        [XmlElement("time_start")]
        public string TimeStart { get; set; }

        [XmlElement("time_expire")]
        public string TimeExpire { get; set; }

        [XmlElement("goods_tag")]
        public string GoodsTag { get; set; }

        [XmlElement("notify_url")]

        public string NotifyUrl { get; set; }
        /// <summary> 
        /// 交易类型   H5 支付 改值为  MWEB
        /// </summary>
        [XmlElement("trade_type")]
        public string TradeType { get; set; }


        [XmlElement("product_id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 指定支付方式  
        /// 参考值  ： no_credit （不使用信用卡支付）
        /// </summary>
        [XmlElement("limit_pay")]
        public string LimitPay { get; set; }
        /// <summary>
        /// trade_type=JSAPI，此参数必传，用户在商户appid下的唯一标识。
        /// openid如何获取，可参考【获取openid】。
        /// 企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，
        /// 再调用【企业号userid转openid接口】进行转换
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }

        [XmlElement("scene_info")]
        public string SceneInfo { get; set; }
    }
}
