using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Sharing.Core.CMQ {
	public class TencentCMQClientFactory {
		private readonly IConfiguration configuration;
		public TencentCMQClientFactory(IConfiguration configuration) {
			this.configuration = configuration;
		}
		public  TencentCMQClient<TModel> Create<TModel>( string prefix)
			where TModel : ICMQMessageHandle {
			var constant = this.configuration.GetWeChatConstant();
			return new TencentCMQClient<TModel>(new ClientMeta
			(
				new EndpointMeta(
					constant.CloudAPI_CMQ__TOPIC_ENDPOINT,
					constant.CloudAPI_CMQ_QUEUE_ENDPOINT,
					constant.CloudAPI_SecrectId,
					constant.CloudAPI_SecretKey),
				prefix
			));
		}
	}
}
