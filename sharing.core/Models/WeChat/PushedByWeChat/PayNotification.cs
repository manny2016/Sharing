

namespace Sharing.WeChat.Models
{
    using System.Xml.Serialization;
    using System.Xml;
    using System;

    [Serializable]
    [XmlRoot("xml")]
    public class PayNotification
    {
        private string appid;
        [XmlElement("appid")]
        public XmlCDataSection AppId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(appid);
            }
            set
            {
                appid = value.Value;
            }
        }

        private string attach;
        [XmlElement("attach")]
        public XmlCDataSection Attach
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(attach);
            }
            set
            {
                attach = value.Value;
            }
        }

        private string bank_type;
        [XmlElement("bank_type")]
        public XmlCDataSection BankType
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(bank_type);
            }
            set
            {
                bank_type = value.Value;
            }
        }

        private int cash_fee;
        [XmlElement("cash_fee")]
        public XmlCDataSection CashFee
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(cash_fee.ToString());
            }
            set
            {
                cash_fee = int.Parse(value.Value);
            }
        }

        private string fee_type;
        [XmlElement("fee_type")]
        public XmlCDataSection FeeType
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(fee_type);
            }
            set
            {
                fee_type = value.Value;
            }
        }
        private string is_subscribe;
        [XmlElement("is_subscribe")]
        public XmlCDataSection IsSubscribe
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(is_subscribe);
            }
            set
            {
                is_subscribe = value.Value;
            }
        }
        private string mch_id;
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

        private string openid;
        [XmlElement("openid")]
        public XmlCDataSection OpenId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(openid);
            }
            set
            {
                openid = value.Value;
            }
        }

        private string out_trade_no;
        [XmlElement("out_trade_no")]
        public XmlCDataSection OutTradeNo
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(out_trade_no);
            }
            set
            {
                out_trade_no = value.Value;
            }
        }

        private string result_code;
        [XmlElement("result_code")]
        public XmlCDataSection ResultCode
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(result_code);
            }
            set
            {
                result_code = value.Value;
            }
        }

        private string return_code;
        [XmlElement("return_code")]
        public XmlCDataSection ReturnCode
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(return_code);
            }
            set
            {
                return_code = value.Value;
            }
        }

        private string sign;
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
        private string time_end;
        [XmlElement("time_end")]
        public XmlCDataSection TimeEnd
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(time_end);
            }
            set
            {
                time_end = value.Value;
            }
        }
        //private int total_fee;
        [XmlElement("total_fee")]
        public int TotalFee
        {
            get; set;
            //get
            //{
            //    XmlDocument doc = new XmlDocument();
            //    return doc.CreateCDataSection(total_fee.ToString());
            //}
            //set
            //{
            //    total_fee = int.Parse(value.Value);
            //}
        }
        private string trade_type;
        [XmlElement("trade_type")]
        public XmlCDataSection TradeType
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(trade_type);
            }
            set
            {
                trade_type = value.Value;
            }
        }
        private string transaction_id;
        [XmlElement("transaction_id")]
        public XmlCDataSection TransactionId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(transaction_id);
            }
            set
            {
                transaction_id = value.Value;
            }
        }
        private string err_code_des;
        [XmlElement("err_code_des")]
        public XmlCDataSection ErrorDescription
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(err_code_des);
            }
            set
            {
                err_code_des = value.Value;
            }
        }
        //https://pay.weixin.qq.com/wiki/doc/api/app/app.php?chapter=9_7&index=3
        //<xml>
        //  <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //  <attach><![CDATA[支付测试]]></attach>
        //  <bank_type><![CDATA[CFT]]></bank_type>
        //  <fee_type><![CDATA[CNY]]></fee_type>
        //  <is_subscribe><![CDATA[Y]]></is_subscribe>
        //  <mch_id><![CDATA[10000100]]></mch_id>
        //  <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
        //  <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
        //  <out_trade_no><![CDATA[1409811653]]></out_trade_no>
        //  <result_code><![CDATA[SUCCESS]]></result_code>
        //  <return_code><![CDATA[SUCCESS]]></return_code>
        //  <sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign>
        //  <sub_mch_id><![CDATA[10000100]]></sub_mch_id>
        //  <time_end><![CDATA[20140903131540]]></time_end>
        //  <total_fee>1</total_fee><coupon_fee><![CDATA[10]]></coupon_fee>
        //<coupon_count><![CDATA[1]]></coupon_count>
        //<coupon_type><![CDATA[CASH]]></coupon_type>
        //<coupon_id><![CDATA[10000]]></coupon_id>
        //<coupon_fee_0><![CDATA[100]]></coupon_fee_0>
        //  <trade_type><![CDATA[JSAPI]]></trade_type>
        //  <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
        //</xml>
    }
}
