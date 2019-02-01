

namespace Sharing.Core.Services
{
    using Sharing.Core;
    using Sharing.WeChat.Models;
    using System.Xml;
    using System;
    using System.Xml.Serialization;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using Dapper;
    using System.Data;
    using Sharing.Core.Models;

    public class WeChatMsgHandler : IWeChatMsgHandler
    {
        private readonly ISharingHostService service;
        private readonly IWeChatUserService wxUserService;
        Dictionary<WeChatEventTypes, Type> dictnoary = new Dictionary<WeChatEventTypes, Type>()
        {
            //{ WeChatEventTypes.card_not_pass_check, typeof(CardCouponAuditkWeChatEventArgs) },
            //{ WeChatEventTypes.card_pass_check, typeof(CardCouponAuditkWeChatEventArgs) },
            { WeChatEventTypes.user_get_card, typeof(GetCardCouponWeChatMsg) },
            //{ WeChatEventTypes.user_gifting_card, typeof(UserGiftingWeChatEventArgs) },
            //{ WeChatEventTypes.user_del_card, typeof(DeleteCardCouponWeChatEventArgs) },
            //{ WeChatEventTypes.user_consume_card, typeof(ConsumCardCouponWeChatArgs) },
            //{ WeChatEventTypes.user_pay_from_pay_cell, typeof(PayFromPayCellWeChatEventArgs) },
            //{ WeChatEventTypes.user_view_card, typeof(ViewCardWeChatEventArgs) },
            //{ WeChatEventTypes.user_enter_session_from_card, typeof(EnterSessinoFromCardWeChatEventArgs) },
            //{ WeChatEventTypes.update_member_card, typeof(UpdateMemberCardWeChatEventArgs) },
            //{ WeChatEventTypes.card_sku_remind, typeof(SkuRemindWeChatEventArgs) },
            //{ WeChatEventTypes.card_pay_order, typeof(PayOrderWeChatEventArgs) },
            //{ WeChatEventTypes.submit_membercard_user_info, typeof(SubmitMemberCardWeChatEventArgs) },
            //{ WeChatEventTypes.card_merchant_check_result, typeof(MerchantAuditWeChatEventArgs) },
            //{ WeChatEventTypes.Nothing, typeof(DoNothingWeChatMsgModel) }
        };
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(WeChatMsgHandler));
        public WeChatMsgHandler(ISharingHostService service, IWeChatUserService wxUserService)
        {
            this.service = service;
            this.wxUserService = wxUserService;
        }
        public void Proccess(IWeChatMsgToken token, string appid)
        {
            var xmlMsg = string.Empty;
            var crypt = new WXBizMsgCrypt(token, appid);
            var rtnVal = crypt.DecryptMsg(token, ref xmlMsg);
            if (rtnVal == 0)
            {
                Logger.InfoFormat("result:{0}", xmlMsg);
                var document = new XmlDocument();
                document.LoadXml(xmlMsg);
                if (Enum.TryParse(document.SelectSingleNode("/xml/MsgType").InnerText,
                    out WeChatMsgTypes msgType))
                {
                    using (var reader = new StringReader(xmlMsg))
                    {
                        if (Enum.TryParse(document.SelectSingleNode("/xml/Event").InnerText, true, out WeChatEventTypes eventtype) == false)
                        {
                            eventtype = WeChatEventTypes.Nothing;
                        }
                        var message = xmlMsg.DeserializeFromXml(dictnoary[eventtype]) as IWeChatMsg;
                        var invoking = GenernateWeChatProccesor(message);
                        invoking(message, appid);
                    }
                }
            }
            else
            {
                Logger.InfoFormat("解密错误:{0}", rtnVal);
            }
        }

        private Action<IWeChatMsg, string> GenernateWeChatProccesor(IWeChatMsg weChatMsg)
        {
            switch (weChatMsg.Event)
            {
                case WeChatEventTypes.user_get_card:
                    return this.UserGetCardProccesor;
            }
            return null;
        }
        #region 微信消息处理器
        private void UserGetCardProccesor(IWeChatMsg message, string appid)
        {            
            this.wxUserService.RegisterCardCoupon(message.Convert(appid));
        }
        #endregion
    }
}
