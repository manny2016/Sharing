


namespace Sharing.Core
{
    using Sharing.WeChat.Models;
    using Newtonsoft.Json.Linq;

    public interface IWeChatApi
    {
        /// <summary>
        /// 获取WeChat 访问  token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        string GetToken(string appid, string secret);
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        T Decrypt<T>(string encryptedData, string iv, string sessionKey);
        /// <summary>
        /// 通过微信用户登陆Code 获取Session
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        SessionWxResponse GetSession(JSCodeApiToken token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="official"></param>
        /// <returns></returns>
        QueryCardCouponWxResponse QueryMCard(IWxApp official);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="official"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        JObject QueryMCardDetails(IWxApp official, IWxCardKey key);

        WxPayParameter Unifiedorder(WxPayData data, string mchid);

    }
}
