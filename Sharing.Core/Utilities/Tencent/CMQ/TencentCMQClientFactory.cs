using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.CMQ {
	public class TencentCMQClientFactory {
		public static TencentCMQClient<TModel> Create<TModel>(string prefix)
			where TModel : ICMQMessageHandle {

			return new TencentCMQClient<TModel>(new ClientMeta
			(
				new EndpointMeta(
					IoC.GetService<WeChatConstant>().CloudAPI_CMQ__TOPIC_ENDPOINT,
					IoC.GetService<WeChatConstant>().CloudAPI_CMQ_QUEUE_ENDPOINT),
				prefix
			));
		}
	}
}
