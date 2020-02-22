
namespace Sharing.Core.CMQ {
	public class EndpointMeta {
		public EndpointMeta(string topic, string queue, string secretId, string secretKey) {
			this.Topic = topic;
			this.Queue = queue;
			this.SecretId = secretId;
			this.SecretKey = secretKey;
		}
		public string Topic { get; private set; }
		public string Queue { get; private set; }
		public string SecretId { get; private set; }
		public string SecretKey { get; private set; }

	}
}
