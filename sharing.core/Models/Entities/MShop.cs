using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Entities
{
    public class MShop
    {
        public virtual long Id { get; set; }
        public virtual long MerchantId { get; set; }
        public virtual long WxShopId { get; set; }
        public virtual string ShopName { get; set; }
        public virtual string Address { get; set; }
    }
}
