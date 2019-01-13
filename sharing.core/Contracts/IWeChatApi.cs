


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
        /// 解密会员卡Code
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        DecryptCodeWxResponse DecryptMCardUserCode(IWxApp app, string encryptedData);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mchid"></param>
        /// <returns></returns>
        WxPayParameter Unifiedorder(WxPayData data, string mchid);

        string GenerateSignForApplyMCard(
            IWxApp official,           
            string cardid,
            long timestamp,
            string nonce_str);
        /// <summary>
        /// 查询用户已经拥有的会员卡
        /// </summary>
        /// <param name="app"></param>
        /// <param name="wxuser"></param>
        /// <param name="mcard"></param>
        QueryWxUserCardResponse QueryWxUserMCards(
            IWxApp app,
            IWxUserOpenId wxuser,
            IWxMCardId mcard = null);


        /// <summary>
        /// 创建或更新卡券
        /// </summary>
        /// <param name="official">商户公众号</param>
        /// <param name="jObject">卡券Json对象</param>
        /// <returns></returns>
        CreateCouponWxResponse SaveOrUpdateCardCoupon(IWxApp official, JObject jObject);

        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <param name="official">商户公众号</param>
        /// <param name="cardId">卡券Id</param>
        /// <returns></returns>
        NormalWxResponse DeleteCardCoupon(IWxApp official, IWxMCardId cardId);
    }
}
