

using Sharing.Core.Models;
using System.Text;

namespace Sharing.Core
{
    public static class OnlineOrderExtension
    {
        public static string GenernateforTakeout(this OnlineOrder model)
        {
            var strBld = new StringBuilder();
            strBld.AppendLine("配送信息");
            strBld.AppendLine($"单号:{model.Code}");
            strBld.AppendLine($"姓名:{model.Name}");
            strBld.AppendLine($"电话:{model.Mobile}");
            strBld.AppendLine($"地址:{model.Address}");
            strBld.AppendLine($"请尽快安排配送,谢谢");
            return strBld.ToString();
        }
        public static string GenernateforPrintDetails(this OnlineOrder model)
        {
            var strBld = new StringBuilder();
            return strBld.ToString();
        }
        public static string GenernateforPrintCode(this OnlineOrder model)
        {
            var strBld = new StringBuilder();
            return strBld.ToString();
        }
    }
}
