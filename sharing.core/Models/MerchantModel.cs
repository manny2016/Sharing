using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class MerchantDetails : IMCode
    {
        public long Id { get; set; }

        public string MCode { get; set; }

        public string BrandName { get; set; }
		[Newtonsoft.Json.JsonIgnore]
        public IWxApp[] Apps
        {
            get
            {
                if (string.IsNullOrEmpty(this.WxApps))
                    return new IWxApp[] { };
                return this.WxApps.DeserializeToObject<WxApp[]>();
            }
        }

        public string WxApps { get; set; }
    }
}
