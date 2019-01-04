
namespace Enjoy.Core.WeChatModels
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    
    [Serializable]
    [XmlRoot("xml")]
    public class WxUnifiedorderResponse
    {
        private string return_code;
        
        [XmlElement("return_code")]
        public XmlCDataSection ReturnCode
        {
            get {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(return_code);
            }
            set {
                return_code = value.Value;
            }
        }
        private string return_msg;
        
        [XmlElement("return_msg")]
        public XmlCDataSection ReturnMsg {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(return_msg);
            }
            set
            {
                return_msg = value.Value;
            }
        }
        private string appid;
        
        [XmlElement("appid")]
        public XmlCDataSection AppId {
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
        private string prepay_id;
        
        [XmlElement("prepay_id")]
        public XmlCDataSection PrepayId
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                return doc.CreateCDataSection(prepay_id);
            }
            set
            {
                prepay_id = value.Value;
            }
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
    }
}