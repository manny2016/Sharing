using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Sharing.Core.CMQ
{
    public class Topic
    {
        private string topicName;
        private ClientMeta ClientMeta;
        internal Topic(string topicName, ClientMeta meta)
        {
            this.topicName = topicName;
            this.ClientMeta = meta;
        }

        public void SetTopicAttributes(int maxMsgSize)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.SetTopicAttributes);
            parameters.Add("topicName", this.topicName);
            if (maxMsgSize < 0 || maxMsgSize > 65536)
                throw new ClientException("Invalid parameterseter maxMsgSize < 0 or maxMsgSize > 65536");
            parameters.Add("maxMsgSize", Convert.ToString(maxMsgSize));

            var signature = Sign.Signature(
                parameters,
                this.ClientMeta.Endpoint.Topic,
                this.ClientMeta.Path,
                this.ClientMeta.SecretKey,
                "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<GeneralCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
            return;
        }


        public TopicMeta GetTopicAttributes()
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.GetTopicAttributes);
            parameters.Add("topicName", this.topicName);

            var signature = Sign.Signature(
                parameters,
                this.ClientMeta.Endpoint.Topic,
                this.ClientMeta.Path,
                this.ClientMeta.SecretKey, "POST",
                this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var url = this.GenerateRequest();
            var result = url.GetUriJsonContent<TopicMeta>((http) =>
             {
                 return http.Wrap(parameters.CreateRequestBody());
             });
            return result.Code.Equals(0) ? result : null;
        }

        public string PublishMessage(string msgBody)
        {
            return PublishMessage(msgBody, new List<string>(), " ");
        }
        public string PublishMessage(string msgBody, string routingKey)
        {
            return PublishMessage(msgBody, new List<string>(), routingKey);
        }
        public string PublishMessage(string msgBody, List<string> tagList, string routingKey)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.PublishMessage);
            parameters.Add("topicName", this.topicName);
            parameters.Add("msgBody", msgBody);
            if (routingKey != "")
                parameters.Add("routingKey", routingKey);

            if (tagList != null)
            {
                for (int i = 0; i < tagList.Count; i++)
                {
                    string k = "msgTag." + Convert.ToString(i + 1);
                    parameters.Add(k, tagList[i]);
                }
            }
            var signature = Sign.Signature(
                parameters, 
                this.ClientMeta.Endpoint.Topic, 
                this.ClientMeta.Path, 
                this.ClientMeta.SecretKey, 
                "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<GeneralMessageCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();

            return result.MsgId;

        }

        public string[] BatchPublishMessage(List<string> vtMsgBody)
        {
            return BatchPublishMessage(vtMsgBody, new List<string>(), "");
        }
        public string[] BatchPublishMessage(List<string> vtMsgBody, string routingKey)
        {
            return BatchPublishMessage(vtMsgBody, new List<string>(), routingKey);
        }
        public string[] BatchPublishMessage(List<string> vMsgBody, List<string> vTagList, string routingKey)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.BatchPublishMessage);
            parameters.Add("topicName", this.topicName);

            if (routingKey != null)
                parameters.Add("routingKey", routingKey);
            if (vMsgBody != null)
            {
                for (int i = 0; i < vMsgBody.Count; i++)
                {
                    string k = "msgBody." + Convert.ToString(i + 1);
                    parameters.Add(k, vMsgBody[i]);
                }
            }
            if (vTagList != null)
            {
                for (int i = 0; i < vTagList.Count; i++)
                {
                    string k = "msgTag" + Convert.ToString(i + 1);
                    parameters.Add(k, vTagList[i]);
                }
            }

            var signature = Sign.Signature(
            parameters,
            this.ClientMeta.Endpoint.Topic,
            this.ClientMeta.Path,
            this.ClientMeta.SecretKey,
            "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<CMQBatchMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
            return result.MsgList ?? new string[] { };

        }

        public int ListSubscription(int offset, int limit, string searchWord, List<string> subscriptionList)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.ListSubscriptionByTopic);
            parameters.Add("topicName", this.topicName);
            if (searchWord != "")
                parameters.Add("searchWord", searchWord);
            if (offset >= 0)
                parameters.Add("offset", Convert.ToString(offset));
            if (limit > 0)
                parameters.Add("limit", Convert.ToString(limit));


            var signature = Sign.Signature(
            parameters,
            this.ClientMeta.Endpoint.Topic,
            this.ClientMeta.Path,
            this.ClientMeta.SecretKey,
            "POST", this.ClientMeta.SignMethod);


            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<SubscriptionListCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
            return result.TotalCount;
        }
        private string GenerateRequest()
        {
            return string.Concat(this.ClientMeta.Endpoint.Topic, this.ClientMeta.Path);
        }
    }
}
