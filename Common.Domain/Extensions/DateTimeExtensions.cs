using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain
{
    public static class DateTimeExtensions
    {
        public static double ToUnixTimeSeconds(this DateTime now)
        {

            var dateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;
            return unixDateTime;
        }

        public static DateTime ToTimeZone(this DateTime now)
        {
            return TimeZoneInfo.ConvertTime(now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }

        public static DateTime TodayZeroHours(this DateTime date)
        {
            return Convert.ToDateTime(date.ToString("dd/MM/yyyy"));
        }

        public static DateTime TodayHoursZeroMs(this DateTime date)
        {
            return Convert.ToDateTime(date.ToString("dd/MM/yyyy HH:mm"));
        }

        public static DateTime TomorrowZeroHours(this DateTime date)
        {
            return date.TodayZeroHours().AddDays(1);
        }

        public static string FormatTempoEmTimeSheet(int totalHours, int totalMinutes)
        {
            return string.Format("{0}:{1}", totalHours.ToString().PadLeft(2, '0'), totalMinutes.ToString().PadLeft(2, '0'));
        }


    }
}
