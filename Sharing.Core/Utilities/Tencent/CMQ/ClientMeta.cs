using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.CMQ {
	public class ClientMeta {
		public EndpointMeta Endpoint { get; set; }

		public string SecretId { get; set; }

		public string SecretKey { get; set; }

		public string SignMethod { get; set; }

		public string Prefix { get; set; }

		public string CurrentVersion { get; set; }
		public string TopicName { get; private set; }
		public string QueueName { get; private set; }
		public string SubscriptionName { get; private set; }
		/// <summary>
		/// 轮询时间单位秒
		/// </summary>
		public int PollingWaitSeconds { get; set; }
		public string Path { get; set; }
		public ClientMeta(
			EndpointMeta endpoint,
			string prefix,
			string path = "/v2/index.php",
			string signMethod = CMQConstant.SignMethod_HMACSHA1,
			int pollingWaitSeconds = 10,
			string currentVersion = "SDK_C#_1.0") {
			this.Endpoint = endpoint;
			this.SecretId = IoC.GetService<WeChatConstant>().CloudAPI_SecrectId;
			this.SecretKey = IoC.GetService<WeChatConstant>().CloudAPI_SecretKey;
			this.SignMethod = signMethod;
			this.Path = path;
			this.PollingWaitSeconds = pollingWaitSeconds;
			this.CurrentVersion = currentVersion;
			this.Prefix = prefix;
			this.TopicName = $"topic_{this.Prefix}";
			this.QueueName = $"queue_{this.Prefix}";
			this.SubscriptionName = $"subscriber_{this.Prefix}";
		}

		public SortedDictionary<string, string> CreateGeneralParameters(string action) {
			var sorted = new SortedDictionary<string, string>();
			Random ran = new Random();
			sorted.Add("Action", action);
			sorted.Add("Nonce", Convert.ToString(new Random().Next(int.MaxValue)));
			sorted.Add("SecretId", this.SecretId);
			var timestamp = DateTime.UtcNow.ToUnixStampDateTime();
			sorted.Add("Timestamp", Convert.ToString(timestamp));
			sorted.Add("RequestClient", this.CurrentVersion);
			sorted.Add("SignatureMethod", this.SignMethod);

			return sorted;
		}
	}
}
