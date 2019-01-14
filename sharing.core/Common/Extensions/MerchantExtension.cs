

namespace Sharing.Core
{
    using Sharing.Core.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class MerchantExtension
    {
        public static IWxApp ChooseMiniprogram(this IEnumerable<MerchantDetails> details, IMCode mcode)
        {
            return details.Where(o => o.MCode.Equals(mcode.MCode))
                .SelectMany(o => o.Apps)
                .FirstOrDefault(o => o.AppType == AppTypes.Miniprogram);
        }
        public static IWxApp ChooseOfficial(this IEnumerable<MerchantDetails> details, IMCode mcode)
        {
            return details.Where(o => o.MCode.Equals(mcode.MCode))
                .SelectMany(o => o.Apps)
                .FirstOrDefault(o => o.AppType == AppTypes.Official);
        }
    }
}
