
namespace Sharing.Core.CMQ
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Text;

    public class Queue
    {
        public string QueueName { get; private set; }
        private ClientMeta ClientMeta { get; set; }
        internal Queue(string queueName, ClientMeta meta)
        {
            this.QueueName = queueName;
            this.ClientMeta = meta;
        }

        public void SetQueueAttributes(QueueMeta meta)
        {

            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.SetQueueAttributes);
            parameters.Add("queueName", this.QueueName);
            if (meta.MaxMsgHeapNum > 0)
                parameters.Add("maxMsgHeapNum", Convert.ToString(meta.MaxMsgHeapNum));
            if (meta.PollingWaitSeconds > 0)
                parameters.Add("pollingWaitSeconds", Convert.ToString(meta.PollingWaitSeconds));
            if (meta.VisibilityTimeout > 0)
                parameters.Add("visibilityTimeout", Convert.ToString(meta.VisibilityTimeout));
            if (meta.MaxMsgSize > 0)
                parameters.Add("maxMsgSize", Convert.ToString(meta.MaxMsgSize));
            if (meta.MsgRetentionSeconds > 0)
                parameters.Add("msgRetentionSeconds", Convert.ToString(meta.MsgRetentionSeconds));

            var signature = Sign.Signature(
                parameters,
                this.ClientMeta.Endpoint.Queue,
                this.ClientMeta.Path,
                this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);
            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                 .GetUriJsonContent<QueueMeta>((http) =>
                 {
                     return http.Wrap(parameters.CreateRequestBody());
                 });
        }

        public QueueMeta GetQueueAttributes()
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.GetQueueAttributes);
            parameters.Add("queueName", this.QueueName);
            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Queue,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var url = this.GenerateRequest();
            var result = url.GetUriJsonContent<QueueMeta>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            return result.Code.Equals(0) ? result : null;
        }

        public string SendMessage(string msgBody)
        {
            return this.SendMessage(msgBody, 0);
        }
        public string SendMessage(string msgBody, int delayTime)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.SendMessage);
            parameters.Add("queueName", this.QueueName);
            parameters.Add("msgBody", msgBody);
            parameters.Add("delaySeconds", Convert.ToString(delayTime));

            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Queue,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                .GetUriJsonContent<GeneralMessageCMQBaseMessage>((http) =>
                {
                    return http.Wrap(parameters.CreateRequestBody());
                });
            result.IfHasErrorThrowException();
            return result.MsgId;
        }

        public string[] BatchSendMessage(List<string> vtMsgBody, int delayTime)
        {
            if (vtMsgBody.Count == 0 || vtMsgBody.Count > 16)
                throw new ClientException("Error: message size is empty or more than 16");
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.BatchSendMessage);
            parameters.Add("queueName", this.QueueName);
            for (int i = 0; i < vtMsgBody.Count; i++)
            {
                string k = "msgBody." + Convert.ToString(i + 1);
                parameters.Add(k, vtMsgBody[i]);
            }
            parameters.Add("delaySeconds", Convert.ToString(delayTime));

            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Queue,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                 .GetUriJsonContent<CMQBatchMessage>((http) =>
                 {
                     return http.Wrap(parameters.CreateRequestBody());
                 });
            result.IfHasErrorThrowException();
            return result.MsgList ?? new string[] { };
        }

        public CMQMessage ReceiveMessage(int pollingWaitSeconds)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.ReceiveMessage);
            parameters.Add("queueName", this.QueueName);
            if (pollingWaitSeconds > 0)
            {
                parameters.Add("UserpollingWaitSeconds", Convert.ToString(pollingWaitSeconds));
                parameters.Add("pollingWaitSeconds", Convert.ToString(pollingWaitSeconds));
            }
            else
            {
                parameters.Add("UserpollingWaitSeconds", Convert.ToString(30000));
            }


            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Queue,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                 .GetUriJsonContent<CMQMessage>((http) =>
                 {
                     return http.Wrap(parameters.CreateRequestBody());
                 });
            if (result.Code == 10200)
                return null;
            return result;
        }

        public CMQMessage[] BatchReceiveMessage(int numOfMsg, int pollingWaitSeconds)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.BatchReceiveMessage);
            parameters.Add("queueName", this.QueueName);
            parameters.Add("numOfMsg", Convert.ToString(numOfMsg));
            if (pollingWaitSeconds > 0)
            {
                parameters.Add("UserpollingWaitSeconds", Convert.ToString(pollingWaitSeconds));
                parameters.Add("pollingWaitSeconds", Convert.ToString(pollingWaitSeconds));
            }
            else
            {
                parameters.Add("UserpollingWaitSeconds", Convert.ToString(30000));
            }
            var signature = Sign.Signature(
              parameters,
              this.ClientMeta.Endpoint.Queue,
              this.ClientMeta.Path,
              this.ClientMeta.SecretKey, "POST", this.ClientMeta.SignMethod);

            parameters.Add("Signature", signature);
            var result = this.GenerateRequest()
                 .GetUriJsonContent<CMQMessageCollection>((http) =>
                 {
                     return http.Wrap(parameters.CreateRequestBody());
                 });

            result.IfHasErrorThrowException();
            return result.MsgInfoList ?? new CMQMessage[] { };
        }

        public void DeleteMessage(string receiptHandle)
        {
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.DeleteMessage);
            parameters.Add("queueName", this.QueueName);
            parameters.Add("receiptHandle", receiptHandle);
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
            return;
        }

        public void BatchDeleteMessage(List<string> vtReceiptHandle)
        {
            if (vtReceiptHandle.Count == 0)
                return;
            var parameters = this.ClientMeta.CreateGeneralParameters(CMQConstant.DeleteMessage);
            parameters.Add("queueName", this.QueueName);
            for (int i = 0; i < vtReceiptHandle.Count; i++)
            {
                string k = "receiptHandle." + Convert.ToString(i + 1);
                parameters.Add(k, vtReceiptHandle[i]);
            }
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
        private string GenerateRequest()
        {
            return string.Concat(this.ClientMeta.Endpoint.Queue, this.ClientMeta.Path);
        }
    }
}
