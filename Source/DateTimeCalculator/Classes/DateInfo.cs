namespace DateTimeCalculator
{
    public struct DateInfo
    {
        public string DayOfWeek { get; }
        public int DayOfMonth { get; }
        public string MonthName { get; }
        public int Year { get; }

        public DateInfo(string dayOfWeek, int dayOfMonth, string monthName, int year)
        {
            DayOfWeek  = dayOfWeek;
            DayOfMonth = dayOfMonth;
            MonthName  = monthName;
            Year       = year;
        }
    }
}
