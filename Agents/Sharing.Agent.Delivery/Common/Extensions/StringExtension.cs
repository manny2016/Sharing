

namespace Sharing.Agent.Delivery
{
    using System.Drawing;
    using System.Linq;
    using System.Collections.Generic;
    public static class StringExtension
    {
        public static SizeF Measure(this char word, Font font)
        {         
            var fakeImage = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(fakeImage);
            return graphics.MeasureString(word.ToString(), font);
        }
        //public static IEnumerable<IEnumerable<char>> Splitlines(
        //    this string text,
        //    float wordWidth,
        //    float maxLineWidth)
        //{
        //    var maxWordsInSingleLine = (int)(maxLineWidth / wordWidth);
        //    var mod = text.ToCharArray().Count() % maxWordsInSingleLine;
        //    var maxLines = mod == 0
        //        ? text.ToCharArray().Count() / maxWordsInSingleLine
        //        : ((text.ToCharArray().Count() - mod) / maxWordsInSingleLine) + 1;
        //    for(var i = 0; i < maxLines; i++)
        //    {

        //    }
        //}

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> array, int size)
        {
            if (array == null || array.Count().Equals(0))
            {
                yield break;
            }
            var skip = 0;
            var take = size;
            while (skip < array.Count())
            {
                yield return array.Skip<T>(skip).Take<T>(take);
                skip += size;
            }
        }

    }
}
