using System;

namespace DateTimeCalculator
{
    public static class TimeCalculator
    {
        public static DateTime AddTime(DateTime date, int hours, int minutes, int seconds)
            => date.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);

        public static TimeSpan AddTime(TimeSpan time1, TimeSpan time2) => time1 + time2;
    }
}
