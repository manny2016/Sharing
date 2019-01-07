using Sharing.Core.Entities;
using Sharing.Core.Models;
using Sharing.WeChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWeChatPayService
    {
        Trade PrepareUnifiedorder(TopupContext context);
        Payment GetPayment(string appid);
    }
}
