using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public class Guard
    {
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (object.Equals(argumentValue, null))
                throw new NullReferenceException(string.Format("The null value of\"{0}\" is not allow", argumentName));
        }
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new NullReferenceException(string.Format("The null and empty value of\"{0}\" is not allow", argumentName));
            }
        }
        public static void ArgumentNotNullOrEmpty(string[] argumentValues, string[] argumentNames)
        {
            var strBld = new StringBuilder();
            for (var index = 0; index < argumentValues.Length; index++)
            {
                if (string.IsNullOrEmpty(argumentValues[index]))
                {
                    strBld.AppendLine(string.Format("The null or empty value of\"{0}\" is not allow",
                        index < argumentNames.Length ? argumentNames[index] : "Unknow"));
                }
            }
            if (strBld.Length > 0)
            {
                throw new NullReferenceException(strBld.ToString());
            }
        }
        public static void ArgumentNotNull(object[] argumentValues, string[] argumentNames)
        {
            var strBld = new StringBuilder();
            for (var index = 0; index < argumentValues.Length; index++)
            {
                if (object.Equals(argumentValues[index], null))
                {
                    strBld.AppendLine(string.Format("The null value of\"{0}\" is not allow",
                        index < argumentNames.Length ? argumentValues[index] : "Unknow"));
                }
            }
            if (strBld.Length > 0)
            {
                throw new NullReferenceException(strBld.ToString());
            }
        }
        public static void ArgumentNotNullOrZero(object[] argumentValues, string argumentName)
        {
            ArgumentNotNull(argumentValues, argumentName);
            if (argumentValues.Length < 1) throw new ArgumentException($"Argement length can not be zero \"{argumentName}\"");
        }

    }
}
