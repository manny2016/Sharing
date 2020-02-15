
using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sharing.Core;
using Sharing.Core.Models;
using Sharing.WeChat.Models;
using Microsoft.Extensions.DependencyInjection;
using Sharing.Core.Services;
using System.Linq;
using System.Text;
using System.IO;
using Sharing.Core.Tests.Models;

namespace Sharing.Core.Tests {

	[TestClass]
	public class WeChatAPITest {
		[TestMethod]
		public void GenernateQRCode() {

			var service = IoC.GetService<IWeChatApi>();

			using ( var stream = new FileStream($@"download/{DateTime.Now.ToUnixStampDateTime()}.png", FileMode.Create) ) {
				var token = service.GetToken("wx6a15c5888e292f99", "a0263dcb8a0fc55485cea4d641fcc2c5");
				var uri = $" https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={token}";
				uri.SaveBinaryFile((http) => {
					http.Method = "POST";
					http.ContentType = "application/json; encoding=utf-8";
					var data = new {
						path = $"pages/welcome/index",
						scene = "sharedBy=2",
						width = 280
					};
					using ( var xx = http.GetRequestStream() ) {
						var body = data.SerializeToJson();
						var buffers = UTF8Encoding.UTF8.GetBytes(body);
						xx.Write(buffers, 0, buffers.Length);
						xx.Flush();
					}
					return http;
				}, stream);
				stream.Flush();
			}


			//using ( var stream = new FileStream($"{Guid.NewGuid().ToString()}.jpg", FileMode.Create) ) {
			//	var buffers = result.Buffer;
			//	stream.Write(buffers, 0, buffers.Length);
			//	stream.Flush();
			//}
		}
		[TestInitialize]
		public void TestInitialize() {
			IoC.ConfigureServiceProvider(null, (configure) => {
				configure.AddMemoryCache();
				configure.AddWeChatApiService();
				configure.AddRandomGenerator();
			});

		}
	}
}
