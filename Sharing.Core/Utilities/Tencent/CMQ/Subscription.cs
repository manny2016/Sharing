using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;



namespace Sharing.Core.CMQ
{
    public class Subscription
    {
        public string TopicName { get; private set; }
        public string SubscriptionName { get; set; }
        private ClientMeta ClientMeta;
        internal Subscription(string topicName, string subscriptionName, ClientMeta meta)
        {
            this.TopicName = topicName;
            this.SubscriptionName = subscriptionName;
            this.ClientMeta = meta;
        }
        public void ClearFilterTags()
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.ClearSUbscriptionFIlterTags);

            parameters.Add("topicName", this.TopicName);
            parameters.Add("subscriptionName", this.SubscriptionName);

            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Queue,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<GeneralCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
        }

        public void SetSubscriptionAttributes(SubscriptionMeta meta)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.SetSubscriptionAttributes);
            parameters.Add("topicName", this.TopicName);
            parameters.Add("subscriptionName", this.SubscriptionName);
            if (meta.NotifyStrategy != "")
                parameters.Add("notifyStrategy", meta.NotifyStrategy);
            if (meta.NotifyContentFormat != "")
                parameters.Add("notifyContentFormat", meta.NotifyContentFormat);
            if (meta.FilterTag != null)
            {
                for (int i = 0; i < meta.FilterTag.Count; ++i)
                    parameters.Add("filterTag." + Convert.ToString(i), meta.FilterTag[i]);
            }
            if (meta.BindingKey != null)
            {
                for (int i = 0; i < meta.BindingKey.Count; ++i)
                    parameters.Add("bindingKey." + Convert.ToString(i), meta.BindingKey[i]);
            }
            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Topic,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);
            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
            .GetUriJsonContent<GeneralCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();

        }

        public SubscriptionMeta GetSubscriptionAttributes()
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.GetSubscriptionAttributes);
            parameters.Add("topicName", this.TopicName);
            parameters.Add("subscriptionName", this.SubscriptionName);

            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Topic,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);
            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<SubscriptionMeta>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });


            return result.Code.Equals(0) ? result : null;

        }
        private string GenerateRequest()
        {
            return string.Concat(this.ClientMeta.Endpoint.Topic, this.ClientMeta.Path);
        }
    }
}
