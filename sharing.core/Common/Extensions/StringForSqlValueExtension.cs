using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public static class StringForSqlValueExtension
    {
        public static string ToSqlValue(this string text)
        {

            if (string.IsNullOrEmpty(text))
                text = string.Empty;
            return string.Format("'{0}'", text.Replace("'", "''"));
        }
    }
}
