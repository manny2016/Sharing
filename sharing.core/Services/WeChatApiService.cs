


namespace Sharing.Core.Services {
	using Microsoft.Extensions.Caching.Memory;
	using Microsoft.Extensions.DependencyInjection;
	using Sharing.WeChat.Models;
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using System.Net;
	using System.Net.Security;
	using System.Security.Cryptography.X509Certificates;
	using Sharing.Core;
	using System.Collections.Generic;
	using System.Linq;
	using System.Collections;
	using System.IO;

	public class WeChatApiService : IWeChatApi {

		private readonly IMemoryCache cache;
		private readonly IRandomGenerator generator;
		public WeChatApiService(IMemoryCache cache, IRandomGenerator generator) {
			this.cache = cache;
			this.generator = generator;
			ServicePointManager.ServerCertificateValidationCallback= new RemoteCertificateValidationCallback(CheckValidationResult);
			
		}
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(WeChatApiService));
		//private readonly IServiceProvider provider = SharingConfigurations.CreateServiceCollection(null).BuildServiceProvider();
		/// <summary>
		/// 获取WeChat api token
		/// </summary>
		/// <param name="appid"></param>
		/// <param name="secret"></param>
		/// <returns></returns>
		public string GetToken(string appid, string secret) {
			return GetToken(appid, secret, false);
			//return this.cache.GetOrCreate<string>(
			//    string.Format("Token_{0}", appid),
			//    (entity) =>
			//    {
			//        var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
			//            appid, secret);
			//        var token = url.GetUriJsonContent<AccessTokenWxResponse>();
			//        entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(token.Expiresin);
			//        return token.Token;
			//    });
		}
		private string GetToken(string appid, string secret, bool forApiTicket = false) {
			return this.cache.GetOrCreate<string>(
				string.Format("Token_{0}_{1}", appid, forApiTicket ? "yes" : "no"),
				(entity) => {
					var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
						appid, secret);
					var token = url.GetUriJsonContent<AccessTokenWxResponse>();
					entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(token.Expiresin);
					return token.Token;
				});
		}
		/// <summary>
		/// 解密数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="encryptedData"></param>
		/// <param name="iv"></param>
		/// <param name="sessionKey"></param>
		/// <returns></returns>
		public T Decrypt<T>(string encryptedData, string iv, string sessionKey) {
			AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

			//设置解密器参数  
			aes.Mode = CipherMode.CBC;
			aes.BlockSize = 128;
			aes.Padding = PaddingMode.PKCS7;
			//格式化待处理字符串  
			byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
			byte[] byte_iv = Convert.FromBase64String(iv);
			byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);

			aes.IV = byte_iv;
			aes.Key = byte_sessionKey;
			//根据设置好的数据生成解密器实例  
			ICryptoTransform transform = aes.CreateDecryptor();

			//解密  
			byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

			//生成结果  
			string result = Encoding.UTF8.GetString(final);

			//反序列化结果，生成用户信息实例  
			return result.DeserializeToObject<T>();
		}

		public SessionWxResponse GetSession(JSCodeApiToken token) {
			return this.cache.GetOrCreate<SessionWxResponse>(
			  string.Format("Session_{0}", token.AppId),
			  (entity) => {
				  var url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&js_code={1}&secret={2}&grant_type=authorization_code",
					token.AppId, token.Code, token.Secret);
				  return url.GetUriJsonContent<SessionWxResponse>();
			  });
		}


		public QueryCardCouponWxResponse QueryMCard(IWxApp official) {
			var url = string.Format("https://api.weixin.qq.com/card/batchget?access_token={0}", GetToken(official.AppId, official.Secret));
			return url.GetUriJsonContent<QueryCardCouponWxResponse>((http) => {
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				var data = new {
					offset = 0,
					count = 10,
					//status_list
					/*
					 * 支持开发者拉出指定状态的卡券列表 
					 * “CARD_STATUS_NOT_VERIFY”, 待审核 ； 
					 * “CARD_STATUS_VERIFY_FAIL”, 审核失败； 
					 * “CARD_STATUS_VERIFY_OK”， 通过审核； 
					 * “CARD_STATUS_DELETE”，  卡券被商户删除； 
					 * “CARD_STATUS_DISPATCH” 在公众平台投放过的卡券；
					 */
					status_list = new string[] { "CARD_STATUS_VERIFY_OK", "CARD_STATUS_DISPATCH" }
				};
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
		}

		public JObject QueryMCardDetails(IWxApp official, IWxCardKey card) {
			var url = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", GetToken(official.AppId, official.Secret));
			return url.GetUriJsonContent<JObject>((http) => {
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				var data = new {
					card_id = card.CardId
				};
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});

		}


		public WxPayParameter Unifiedorder(WxPayData data, string mchid) {
			var request = "https://api.mch.weixin.qq.com/pay/unifiedorder";
			var order = request.GetUriContentDirectly((http) => {
				
				http.Timeout = 30 * 1000;
				ServicePointManager.DefaultConnectionLimit = 200;
				http.UserAgent = string.Format("WXPaySDK/{3} ({0}) .net/{1} {2}",
					Environment.OSVersion, Environment.Version, mchid,
					typeof(WxPayParameter).Assembly.GetName().Version);
				http.Method = "POST";
				http.ContentType = "text/xml";
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToXml();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			}).DeserializeFromXml<WeChatUnifiedorderResponse>();
			return new WxPayParameter(order, this.generator);
		}


		public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {
			//直接确认，否则打不开    
			return true;
		}

		public string GenerateSignForApplyMCard(
			IWxApp official,
			IWxApp miniprogram,
			string cardid,
			long timestamp,
			string nonce_str) {
			//https://mp.weixin.qq.com/debug/cgi-bin/sandbox?t=cardsign
			//卡券签名算法  https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141115
			var api_ticket = GetApiTicket(miniprogram.AppId, this.GetToken(official.AppId, official.Secret));
			ArrayList AL = new ArrayList();
			AL.Add(api_ticket);
			AL.Add(timestamp);
			AL.Add(nonce_str);
			AL.Add(cardid);
			AL.Sort(new DictionarySort());

			//var perpare = string.Format("{0}{1}{2}{3}", timestamp, nonce_str,api_ticket, cardid );
			var perpare = string.Join(string.Empty, AL.ToArray());
			return perpare.GetSHA1Crypto();

		}

		private string GetApiTicket(string appid, string token) {
			string cacheKey = string.Format("ticket-{0}", appid);
			return this.cache.GetOrCreate<string>(cacheKey,
				(entity) => {
					//https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token=$accessToken
					//https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=ACCESS_TOKEN&type=wx_card
					var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=wx_card", token);
					var response = url.GetUriJsonContent<TicketWxResponse>();
					entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(response.Expiresin);
					return response.Ticket;
				});
		}

		public QueryWxUserCardResponse QueryWxUserMCards(
			IWxApp app,
			IWxUserOpenId wxuser,
			IWxMCardId mcard = null) {
			var url = string.Format("https://api.weixin.qq.com/card/user/getcardlist?access_token={0}"
				, GetToken(app.AppId, app.Secret));
			return url.GetUriJsonContent<QueryWxUserCardResponse>((http) => {
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				var data = new {
					openid = wxuser.OpenId,
					card_id = (mcard == null || string.IsNullOrWhiteSpace(mcard.CardId))
					? null
					: mcard.CardId
				};
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
		}

		public DecryptCodeWxResponse DecryptMCardUserCode(IWxApp app, string encryptedData) {
			string url = string.Format("https://api.weixin.qq.com/card/code/decrypt?access_token={0}", GetToken(app.AppId, app.Secret));
			return url.GetUriJsonContent<DecryptCodeWxResponse>((http) => {
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				var data = new {
					encrypt_code = encryptedData
				};
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
		}

		public CreateCouponWxResponse SaveOrUpdateCardCoupon(IWxApp official, JObject jObject) {

			var url = string.Format(string.IsNullOrEmpty(jObject.ParseCardId())
				? "https://api.weixin.qq.com/card/create?access_token={0}"
				: "https://api.weixin.qq.com/card/update?access_token={0}",
				GetToken(official.AppId, official.Secret));
			return url.GetUriJsonContent<CreateCouponWxResponse>((http) => {
				http.Method = "POST";
				http.ContentType = "application/json;encoding=utf-8";
				using ( var stream = http.GetRequestStream() ) {
					var buffers = UTF8Encoding.UTF8.GetBytes(jObject.ToString());
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
		}



		public NormalWxResponse DeleteCardCoupon(IWxApp official, IWxMCardId cardId) {
			var url = string.Format("https://api.weixin.qq.com/card/delete?access_token={0}", GetToken(official.AppId, official.Secret));
			return url.GetUriJsonContent<NormalWxResponse>((http) => {
				var data = new { card_id = cardId.CardId };
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
		}

		public RedpackResponse SendRedpack(IWxApp official, string payKey, Redpack redpack) {
			var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
			var sign = string.Concat(redpack.PrepareSign(), $"&key={payKey}").MakeSign(WxPayData.SIGN_TYPE_MD5, payKey);
			redpack.SetSign(sign);
			
			return url.GetUriContentDirectly((http) => {
				http.Method = "POST";
				http.ContentType = "text/xml";
				var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
				store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly | OpenFlags.MaxAllowed);
				var path  = Path.Combine(Environment.CurrentDirectory, @"cert\apiclient_cert.p12");
				var cert = new X509Certificate2(path, "1520961881");
				//var cert = store.Certificates.Find(X509FindType.FindBySerialNumber, "7d4daceb0866305aed424a175e86c005e6b80ee3", false);
				http.ClientCertificates.Add(cert);
				using ( var stream = http.GetRequestStream() ) {
					var body = redpack.SerializeToXml();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			}).DeserializeFromXml<RedpackResponse>();
			
		}

		public QueryWxUserDetailsResponse QueryAllWxUsers(QueryWxUserInfoRequest context) {
			var url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}{1}";
			var list = string.Format(url,
				this.GetToken(context.WxApp.AppId, context.WxApp.Secret),
				string.IsNullOrEmpty(context.NextOpenId) ? string.Empty : $"&next_openid={context.NextOpenId}")
				.GetUriJsonContent<QueryWxUserListResponse>();
			if ( list.Data == null || list.Data.OpenIds == null || list.Data.OpenIds.Length == 0 ) {
				return new QueryWxUserDetailsResponse() {
					NextOpenId = null,
					WeChatUserInfos = new WeChatUserInfo[] { }
				};
			}
			var infos = new List<WeChatUserInfo>();
			foreach ( var package in list.Data.OpenIds.Split<string>(100) ) {
				var result = $"https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={this.GetToken(context.WxApp.AppId, context.WxApp.Secret)}"
					  .GetUriJsonContent<JObject>((http) => {
						  http.Method = "POST";
						  var data = new {
							  user_list = package.Select((x) => {
								  return new {
									  openid = x,
									  lang = "zh_CN"
								  };
							  })
						  };
						  using ( var stream = http.GetRequestStream() ) {
							  var body = data.SerializeToJson();
							  var buffers = UTF8Encoding.UTF8.GetBytes(body);
							  stream.Write(buffers, 0, buffers.Length);
							  stream.Flush();
						  }
						  return http;
					  });
				var lt = result.TryGetValues<WeChatUserInfo>("$.user_info_list").ToArray();
				infos.AddRange(lt);
			}
			return new QueryWxUserDetailsResponse() {
				NextOpenId = list.NextOpenId,
				WeChatUserInfos = infos.ToArray()
			};
		}

		public NormalWxResponse SendWeChatMessage(IWxApp official, string openid, string text) {
			var data = new CustomMessageContext() {
				MsgType = "text",
				Text = new CustomMessageContent() { Content = text },
				ToUser = openid
			};
			var url = $"https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={this.GetToken(official.AppId, official.Secret)}";
			return url.GetUriJsonContent<NormalWxResponse>((http) => {
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
		}

		public void GenernateSharedMomentsPoster(Stream stream, IWxApp app, IWxUserKey sharedBy) {

			var uri = $"https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={GetToken(app.AppId, app.Secret)}";
			uri.SaveBinaryFile((http) => {
				http.Method = "POST";
				http.ContentType = "application/json; encoding=utf-8";
				var data = new {
					path = $"pages/welcome/index",
					scene = sharedBy.OpenId,
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
		}
	}
}
