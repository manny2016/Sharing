using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.WeChat.Models
{
    public class DictionarySort : System.Collections.IComparer
    {
        public int Compare(object oLeft, object oRight)
        {
            string sLeft = oLeft.ToString();
            string sRight = oRight.ToString();
            int iLeftLength = sLeft.Length;
            int iRightLength = sRight.Length;
            int index = 0;
            while (index < iLeftLength && index < iRightLength)
            {
                if (sLeft[index] < sRight[index])
                    return -1;
                else if (sLeft[index] > sRight[index])
                    return 1;
                else
                    index++;
            }
            return iLeftLength - iRightLength;
        }
    }
}
