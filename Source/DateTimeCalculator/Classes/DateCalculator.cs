using System;
using System.Globalization;

namespace DateTimeCalculator
{
    public static class DateCalculator
    {
        public static DateResults ConvertDate(DateTime date)
            => new DateResults(GetDate(date, Constants.PersianCalendar), GetDate(date, Constants.HijriCalendar), GetDate(date, Constants.GregorianCalendar));

        public static TimeSpan GetDifference(DateTime date1, DateTime date2) => date1 - date2;

        public static string AddToDate(DateTime date, Calendar cal, int numYears, int numMonths, int numDays)
            => GetDate(date.AddYears(numYears).AddMonths(numMonths).AddDays(numDays), cal);

        public static DateTime AddToDate(DateTime date, int numYears, int numMonths, int numDays)
            => date.AddYears(numYears).AddMonths(numMonths).AddDays(numDays);

        public static Calendar GetCalendar(int index)
        {
            // As defined in MainWindow.xaml
            switch (index)
            {
                case 0: return Constants.PersianCalendar;
                case 1: return Constants.HijriCalendar;
                case 2: return Constants.GregorianCalendar;
                default: throw new ArgumentOutOfRangeException(nameof(index), ".تقویم انتخاب شده نامعتبر است");
            }
        }

        public static DateInfo GetDateInfo(DateTime date, Calendar cal)
        {
            string dayOfWeek = Constants.Days[(int)cal.GetDayOfWeek(date)];
            int dayOfMonth = cal.GetDayOfMonth(date);
            string monthName = GetMonthName();
            int year = cal.GetYear(date);

            return new DateInfo(dayOfWeek, dayOfMonth, monthName, year);

            string GetMonthName()
            {
                int monthIndex = cal.GetMonth(date) - 1;

                if (cal == Constants.PersianCalendar)
                {
                    return Constants.PersianMonths[monthIndex];
                }
                else if (cal == Constants.HijriCalendar)
                {
                    return Constants.HijriMonths[monthIndex];
                }
                return Constants.GregorianMonths[monthIndex];
            }
        }

        private static string GetDate(DateTime date, Calendar cal)
        {
            int year = cal.GetYear(date);
            int month = cal.GetMonth(date);
            int day = cal.GetDayOfMonth(date);

            return $"{AddLeadingZero(year)}/{AddLeadingZero(month)}/{AddLeadingZero(day)}";

            string AddLeadingZero(int value) => (value < 10) ? ("0" + value) : value.ToString();
        }
    }
}
