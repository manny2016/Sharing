using Sharing.Core.Configuration;
using System;
using System.Configuration;
using Sharing.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sharing.WeChat.Models;
using System.Threading;
using Sharing.Core.Models;
using Sharing.Core.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Sendredpack();
        }
        static void Sendredpack()
        {
           
        }
        static void QueryWeChatUser()
        {
            var url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN";
        }
    }
}
