namespace Sharing.Core
{
    using Entity = Sharing.Core.Entities;
    using Sharing.Core.Models;
    using System.Collections.Generic;
    using Sharing.Core.Entities;


    public interface IWeChatUserService
    {
        /// <summary>
        /// 登记一个微信用户，并返回平台Id
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// 返回微信用户在本平台的流水号
        /// </returns>
        Membership Register(RegisterWxUserContext context);

        void RegisterCardCoupon(RegisterCardCoupon registerCard);

        IList<ISharedContext> GetSharedContext(IMchId app);

     
        
    }
}
