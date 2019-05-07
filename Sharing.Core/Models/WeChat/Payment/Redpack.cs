

using System.Xml;
using System.Xml.Serialization;

namespace Sharing.WeChat.Models
{
    /// <summary>
    /// 现金红包 实体类
    /// https://pay.weixin.qq.com/wiki/doc/api/tools/cash_coupon.php?chapter=13_4&index=3
    /// </summary>
    /// <remarks>
    /// 数据示例如下
    /// <xml><sign><![CDATA[E1EE61A91C8E90F299DE6AE075D60A2D]]></sign>
    /// <mch_billno><![CDATA[0010010404201411170000046545]]></mch_billno>
    /// <mch_id><![CDATA[888]]></mch_id><wxappid><![CDATA[wxcbda96de0b165486]]></wxappid>
    /// <send_name><![CDATA[send_name]]></send_name>
    /// <re_openid><![CDATA[onqOjjmM1tad-3ROpncN-yUfa6uI]]></re_openid><total_amount><![CDATA[200]]></total_amount>
    /// <total_num><![CDATA[1]]></total_num><wishing><![CDATA[恭喜发财]]></wishing>
    /// <client_ip><![CDATA[127.0.0.1]]></client_ip><act_name><![CDATA[新年红包]]></act_name>
    /// <remark><![CDATA[新年红包]]></remark><scene_id><![CDATA[PRODUCT_2]]></scene_id>
    /// <nonce_str><![CDATA[50780e0cca98c8c8e814883e5caa672e]]></nonce_str>
    /// <risk_info>posttime%3d123123412%26clientversion%3d234134%26mobile%3d122344545%26deviceid%3dIOS</risk_info></xml>
    /// </remarks>
    [XmlRoot("xml")]
    public class Redpack
    {
        public Redpack() { }
        /// <summary>
        /// 随机字符串	nonce_str	是	5K8264ILTKCH16CQ2502SI8ZNMTM67VS	String(32)	随机字符串，不长于32位
        /// </summary>
        public Redpack(
            string nonce_str,
            string mch_billno,
            string mch_id,
            string wxappid,
            string send_name,
            string re_openid,
            int total_amount,
            int total_num,
            string wishing,
            string client_ip,
            string act_name,
            string remark)
        {
            this.nonce_str = nonce_str;
            this.mch_billno = mch_billno;
            this.mch_id = mch_id;
            this.wxappid = wxappid;
            this.send_name = send_name;
            this.re_openid = re_openid;
            this.total_amount = total_amount;
            this.total_num = total_num;
            this.wishing = wishing;
            this.client_ip = client_ip;
            this.act_name = act_name;
            this.remark = remark;

        }
        private string nonce_str;
        [XmlElement("nonce_str")]
        public XmlCDataSection NonceStr
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(nonce_str);
            }
            set
            {
                nonce_str = value.Value;
            }
        }

        private string sign;
        /// <summary>
        /// 签名	sign	是	C380BEC2BFD727A4B6845133519F3AD6	String(32)	详见签名生成算法
        /// </summary>
        [XmlElement("sign")]
        public XmlCDataSection Sign
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(sign);
            }
            set
            {
                sign = value.Value;
            }
        }
        /// <summary>
        /// 商户订单号	mch_billno	是	10000098201411111234567890	String(28)	        商户订单号（每个订单号必须唯一。取值范围：0~9，a ~z，A ~Z）接口根据商户订单号支持重入，如出现超时可再调用。
        /// </summary>
        private string mch_billno;
        [XmlElement("mch_billno")]
        public XmlCDataSection MchBillno
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(mch_billno);
            }
            set
            {
                mch_billno = value.Value;
            }
        }
        private string mch_id;
        /// <summary>
        /// 商户号	mch_id	是	10000098	String(32)	微信支付分配的商户号
        /// </summary>

        [XmlElement("mch_id")]
        public XmlCDataSection MchId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(mch_id);
            }
            set
            {
                mch_id = value.Value;
            }
        }
        private string wxappid;
        /// <summary>
        /// 公众账号appid	wxappid	是	wx8888888888888888	String(32)	微信分配的公众账号ID（企业号corpid即为此appId）。在微信开放平台（open.weixin.qq.com）申请的移动应用appid无法使用该接口。
        /// </summary>
        [XmlElement("wxappid")]
        public XmlCDataSection WxAppId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(wxappid);
            }
            set
            {
                wxappid = value.Value;
            }
        }
        private string send_name;
        /// <summary>
        /// 商户名称	send_name	是	天虹百货	String(32)	        红包发送者名称        注意：敏感词会被转义成字符*
        /// </summary>
        [XmlElement("send_name")]
        public XmlCDataSection SendName
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(send_name);
            }
            set
            {
                send_name = value.Value;
            }
        }
        private string re_openid;
        /// <summary>
        /// 用户openid	re_openid	是	oxTWIuGaIt6gTKsQRLau2M0yL16E	String(32)	       接受红包的用户openid        openid为用户在wxappid下的唯一标识（获取openid参见微信公众平台开发者文档：网页授权获取用户基本信息）
        /// </summary>
        [XmlElement("re_openid")]
        public XmlCDataSection ReOpenId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(re_openid);
            }
            set
            {
                re_openid = value.Value;
            }
        }
        private int total_amount;
        /// <summary>
        /// 付款金额	total_amount	是	1000	int	付款金额，单位分
        /// </summary>
        [XmlElement("total_amount")]
        public XmlCDataSection TotalAmount
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(total_amount.ToString());
            }
            set
            {
                total_amount = int.Parse(value.Value);
            }
        }
        private int total_num;
        /// <summary>
        /// 红包发放总人数	total_num	是	1	int	        红包发放总人数        total_num = 1
        /// </summary>
        [XmlElement("total_num")]
        public XmlCDataSection TotalNum
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(total_num.ToString());
            }
            set
            {
                total_num = int.Parse(value.Value);
            }
        }
        private string wishing;
        /// <summary>
        /// 红包祝福语	wishing	是	感谢您参加猜灯谜活动，祝您元宵节快乐！	String(128)	       红包祝福语        注意：敏感词会被转义成字符*
        /// </summary>
        [XmlElement("wishing")]
        public XmlCDataSection Wishing
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(wishing);
            }
            set
            {
                wishing = value.Value;
            }
        }
        private string client_ip;
        /// <summary>
        /// Ip地址	client_ip	是	192.168.0.1	String(15)	调用接口的机器Ip地址
        /// </summary>
        [XmlElement("client_ip")]
        public XmlCDataSection ClientIP
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(client_ip);
            }
            set
            {
                client_ip = value.Value;
            }
        }
        private string act_name;
        /// <summary>
        /// 动名称	act_name	是	猜灯谜抢红包活动	String(32)	        活动名称        注意：敏感词会被转义成字符*
        /// </summary>
        [XmlElement("act_name")]
        public XmlCDataSection ActName
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(act_name);
            }
            set
            {
                act_name = value.Value;
            }
        }
        private string remark;
        /// <summary> remark
        /// 备注	remark	是	猜越多得越多，快来抢！	String(256)	备注信息
        /// 场景id	scene_id	否	PRODUCT_8	String(32)	        
        /// </summary>        
        [XmlElement("remark")]
        public XmlCDataSection Remark
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(remark);
            }
            set
            {
                remark = value.Value;
            }
        }
        /// <summary>
        /// 活动信息	risk_info	否	posttime%3d123123412%26clientversion%3d234134%26mobile%3d122344545%26deviceid%3dIOS	String(128)	
        /// posttime:用户操作的时间戳
        /// mobile:业务系统账号的手机号，国家代码-手机号。不需要+号
        /// deviceid :mac 地址或者设备唯一标识
        /// clientversion :用户操作的客户端版本
        /// 把值为非空的信息用key = value进行拼接，再进行urlencode
        ///  urlencode(posttime= xx & mobile = xx & deviceid = xx)
        /// </summary>
        //[XmlElement("risk_info")]
        //public XmlCDataSection risk_info { get; set; }

        public void SetSign(string sign)
        {
            this.sign = sign.ToUpper();
        }
    }
}
