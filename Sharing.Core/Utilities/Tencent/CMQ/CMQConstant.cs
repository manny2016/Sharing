using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.CMQ
{
    public class CMQConstant
    {
        public const string ListQueue = "ListQueue";
        public const string GetTopicAttributes = "GetTopicAttributes";
        public const string SetTopicAttributes = "SetTopicAttributes";
        public const string PublishMessage = "PublishMessage";
        public const string BatchPublishMessage = "BatchPublishMessage";
        public const string ListSubscriptionByTopic = "ListSubscriptionByTopic";
        public const string CreateQueue = "CreateQueue";
        public const string DeleteQueue = "DeleteQueue";
        public const string Unsubscribe = "Unsubscribe";
        public const string GetQueueAttributes = "GetQueueAttributes";
        public const string SetQueueAttributes = "SetQueueAttributes";
        public const string SendMessage = "SendMessage";
        public const string BatchSendMessage = "BatchSendMessage";
        public const string ReceiveMessage = "ReceiveMessage";
        public const string BatchReceiveMessage = "BatchReceiveMessage";
        public const string DeleteMessage = "DeleteMessage";
        public const string BatchDeleteMessage = "BatchDeleteMessage";
        public const string ClearSUbscriptionFIlterTags = "ClearSUbscriptionFIlterTags";
        public const string SetSubscriptionAttributes = "SetSubscriptionAttributes";
        public const string GetSubscriptionAttributes = "GetSubscriptionAttributes";
        public const string CreateTopic = "CreateTopic";
        public const string Subscribe = "Subscribe";
        public const string SignMethod_HMACSHA1 = "HmacSHA1";
        public const string SignMethod_HMACSHA256 = "HmacSHA256";
    }
}
