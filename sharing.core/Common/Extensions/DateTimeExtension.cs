namespace Sharing.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class DateTimeExtension
    {
        public static readonly DateTime UNIX_START_DATE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly long STICK_HOURLY = 60 * 60;
        public static long ToUnixStampDateTime(this DateTime dateTime)
        {
            return ((dateTime.Ticks - UNIX_START_DATE.Ticks) / 10000000L) + (3600 * 8);
        }
       
        public static DateTime ToDateTimeFromUnixStamp(this long timestamp)
        {
            return UNIX_START_DATE.Add(TimeSpan.FromTicks(timestamp * 10000000L));
        }
        public static string ToDateTimeString(this long timestamp)
        {
            if (timestamp.Equals(0)) return string.Empty;
            return timestamp.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd HH:mm");
        }
        public static string ToDateString(this long timestamp)
        {
            if (timestamp.Equals(0)) return string.Empty;
            return timestamp.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd");
        }
        public static DateTime ToDateTimeFromTwitter(this string time)
        {
            return DateTime.ParseExact(
                time,
                "ddd MMM dd HH:mm:ss zzzz yyyy",
                CultureInfo.InvariantCulture);
        }
        public static DateTime ToDateTime(this string datetime)
        {
            return DateTime.Parse(datetime);
        }
        public static TimeZoneInfo ToTimeZoneInfoFromTwitter(this string timezone)
        {
            return TimeZoneInfo.GetSystemTimeZones()
                .FirstOrDefault(t => string.Join(" ", t.DisplayName.Split(' ').Skip(1)) == timezone);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EnumerateAllHours(this DateTime from, DateTime to)
        {
            DateTime dtBegin = from.Date.AddHours(from.Hour);
            DateTime dtEnd = to.Date.AddHours(to.Hour);
            while (dtBegin <= dtEnd)
            {
                DateTime item = from;
                dtBegin = dtBegin.AddHours(1);
                yield return item;
            }
        }
        public static DateTime GetStartOfToday(this DateTime today)
        {
            return today.Date;
        }

    }
}