using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharing.Core.Services;
namespace Sharing.Core.Tests
{
    [TestClass]
    public class WeChatApiTest
    {
        private IServiceProvider provider = SharingConfigurations.CreateServiceCollection()
                .AddWeChatApiService()
                .AddMcardService()
                .BuildServiceProvider();
        [TestMethod]
        public void SynchronousMemberCards()
        {
            var service = provider.GetService<IMCardService>();
            service.Synchronous();
        }
    }
}
