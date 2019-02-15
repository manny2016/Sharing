
namespace Sharing.Portal.Api
{

    using System.Linq;
    using System.Collections.Generic;
    using Sharing.Core.Models;

    public static class ProductExension
    {
        public static string ToDisplayMoney(this int systemMoney)
        {
            return ((float)(systemMoney / 100)).ToString();
        }
        public static int ToSystemMoney(this string dispalyMoney)
        {
            if(int.TryParse(dispalyMoney, out int iPrice))
            {
                return iPrice * 100;
            }
            return 0;
        }
    }
}
