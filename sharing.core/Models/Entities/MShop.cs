using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Entities
{
    public class MShop
    {
        public  long Id { get; set; }
        public  long MerchantId { get; set; }
        public  long WxShopId { get; set; }
        public  string ShopName { get; set; }
        public  string Address { get; set; }
    }
}
