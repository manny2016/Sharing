using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharing.Core.Services;
using Sharing.WeChat.Models;

namespace Sharing.Core.Tests
{
    [TestClass]
    public class WeChatApiTest
    {
        //private IServiceProvider provider = SharingConfigurations.CreateServiceProvider((collection) =>
        //{
        //    collection.AddMcardService();
        //    collection.AddWeChatUserService();
        //    collection.AddWeChatPayService();
        //});
        [TestMethod]
        public void SynchronousMemberCards()
        {
            //var service = provider.GetService<IMCardService>();
            //service.Synchronous();
        }
        [TestMethod]
        public void PrepareUnifiedorder()
        {
            var context = new TopupContext()
            {
                AppId = "wx6a15c5888e292f99",
                CardId = "p18KQ51iyqwX2FXNMJlQgM4TN1o0",
                OpenId = "o_SjX5Yt_H5En9323Syhw1Aic3Jk",
                MCode = "92511402MA6941EG0R",
                Code = "cccc",
                Money = 1
            };
            //var service = provider.GetService<IWeChatPayService>();
            //var trade = service.PrepareUnifiedorder(context);
        }

    }
}
