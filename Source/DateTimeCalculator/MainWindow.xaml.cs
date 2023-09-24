using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;

namespace DateTimeCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FillControls();
            SetControlValues();

            void FillControls()
            {
                // Date calculator
                InternalFillControls(DateConverter_Day, 31);
                InternalFillControls(DateConverter_Month, 12);

                InternalFillControls(DateDifference_DayFrom, 31);
                InternalFillControls(DateDifference_DayTo, 31);
                InternalFillControls(DateDifference_MonthFrom, 12);
                InternalFillControls(DateDifference_MonthTo, 12);

                InternalFillControls(DateAddSub_Day, 31);
                InternalFillControls(DateAddSub_Month, 12);

                // Time calculator
                InternalFillControls(TimeAddSub_HourFrom, 99, 0);
                InternalFillControls(TimeAddSub_HourTo, 99, 0);
                InternalFillControls(TimeAddSub_MinuteFrom, 59, 0);
                InternalFillControls(TimeAddSub_MinuteTo, 59, 0);
                InternalFillControls(TimeAddSub_SecondFrom, 59, 0);
                InternalFillControls(TimeAddSub_SecondTo, 59, 0);
            }

            void InternalFillControls(ComboBox c, byte maxChoices, byte start = 1)
            {
                for (byte i = start; i <= maxChoices; i++)
                    c.Items.Add(i < 10 ? ("0" + i) : i.ToString());  // Add a leading zero if necessary
            }

            void SetControlValues()
            {
                SetDateConverterLabels(Constants.ValuePlaceholder, Constants.ValuePlaceholder, Constants.ValuePlaceholder);
                DateDifference_Result.Text = Constants.ValuePlaceholder;

                DateAddSub_Result.Text = Constants.ValuePlaceholder;
                TimeAddSub_Result.Text = Constants.ValuePlaceholder;

                TimeAddSub_HourFrom.SelectedIndex   = DateTime.Now.Hour;
                TimeAddSub_MinuteFrom.SelectedIndex = DateTime.Now.Minute;
                TimeAddSub_SecondFrom.SelectedIndex = DateTime.Now.Second;
            }
        }

        private void DateConverter_Convert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cal   = DateCalculator.GetCalendar(DateConverter_Calendar.SelectedIndex);
                int year  = int.Parse(DateConverter_Year.Text);
                int month = int.Parse(DateConverter_Month.Text);
                int day   = int.Parse(DateConverter_Day.Text);

                var results = DateCalculator.ConvertDate(new DateTime(year, month, day, cal));

                SetDateConverterLabels(results.ResultPersian, results.ResultHijri, results.ResultGregorian);
                SetDateConverterToolTips(new DateTime(year, month, day, cal));
            }
            catch (Exception ex)
            {
                SetDateConverterLabels(Constants.ValuePlaceholder, Constants.ValuePlaceholder, Constants.ValuePlaceholder);
                SetDateConverterToolTips();

                ShowMessagebox(ex.Message, ".لطفا تاریخ را به درستی وارد کنید");
            }
        }

        private void DateConverter_Today_Click(object sender, RoutedEventArgs e)
        {
            var results = DateCalculator.ConvertDate(DateTime.Now);

            SetDateConverterLabels(results.ResultPersian, results.ResultHijri, results.ResultGregorian);
            SetDateConverterToolTips(DateTime.Now);
        }

        private void SetDateConverterLabels(string persianDate, string hijriDate, string gregorianDate)
        {
            DateConverter_ToPersian.Text   = persianDate;
            DateConverter_ToHijri.Text     = hijriDate;
            DateConverter_ToGregorian.Text = gregorianDate;
        }

        private void SetDateConverterToolTips()
        {
            // Erase the tooltips
            DateConverter_ToPersian.ToolTip = null;
            DateConverter_ToHijri.ToolTip = null;
            DateConverter_ToGregorian.ToolTip = null;
        }

        private void SetDateConverterToolTips(DateTime date)
        {
            SetToolTip(DateConverter_ToPersian, DateCalculator.GetDateInfo(date, Constants.PersianCalendar));
            SetToolTip(DateConverter_ToHijri, DateCalculator.GetDateInfo(date, Constants.HijriCalendar));
            SetToolTip(DateConverter_ToGregorian, DateCalculator.GetDateInfo(date, Constants.GregorianCalendar));
        }

        private void DateConverter_CopyResult_Click(object sender, RoutedEventArgs e)
        {
            // Avoid copying the results if a successful date conversion isn't performed yet
            if (DateConverter_ToPersian.Text == Constants.ValuePlaceholder)
                return;

            switch ((sender as Button).Name)
            {
                case "DateConverter_CopyPersian":
                    Clipboard.SetText(DateConverter_ToPersian.Text);
                    break;

                case "DateConverter_CopyHijri":
                    Clipboard.SetText(DateConverter_ToHijri.Text);
                    break;

                case "DateConverter_CopyGregorian":
                    Clipboard.SetText(DateConverter_ToGregorian.Text);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sender));
            }
        }

        private void DateDifference_Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cal = DateCalculator.GetCalendar(DateDifference_Calendar.SelectedIndex);

                int year1  = int.Parse(DateDifference_YearFrom.Text);
                int year2  = int.Parse(DateDifference_YearTo.Text);
                int month1 = int.Parse(DateDifference_MonthFrom.Text);
                int month2 = int.Parse(DateDifference_MonthTo.Text);
                int day1   = int.Parse(DateDifference_DayFrom.Text);
                int day2   = int.Parse(DateDifference_DayTo.Text);

                var date1 = new DateTime(year1, month1, day1, cal);
                var date2 = new DateTime(year2, month2, day2, cal);

                var diff = DateCalculator.GetDifference(date1, date2);

                DateDifference_Result.Text = $"{Math.Abs(diff.TotalDays):N0} روز ({Math.Abs(diff.TotalDays / 365):F2} سال)";

                // Fetch extra information about the result and set the tooltip
                // Source: https://www.codesansar.com/c-programming-examples/convert-number-days-years-months-days.htm
                int totalDays = (int)Math.Abs(diff.TotalDays);
                int years  = totalDays / 365;
                int months = (totalDays - years * 365) / 30;
                int days   = totalDays - years * 365 - months * 30;

                SetToolTip(DateDifference_Result, $"{years} سال، {months} ماه، {days} روز");
            }
            catch (Exception ex)
            {
                DateDifference_Result.Text = Constants.ValuePlaceholder;
                SetToolTip(DateDifference_Result, null);

                ShowMessagebox(ex.Message, ".لطفا تواریخ را به درستی وارد کنید");
            }
        }

        private void DateAddSub_Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cal = DateCalculator.GetCalendar(DateAddSub_Calendar.SelectedIndex);

                int year      = int.Parse(DateAddSub_Year.Text);
                int month     = int.Parse(DateAddSub_Month.Text);
                int day       = int.Parse(DateAddSub_Day.Text);
                int numYears  = int.Parse(DateAddSub_AddYear.Text);
                int numMonths = int.Parse(DateAddSub_AddMonth.Text);
                int numDays   = int.Parse(DateAddSub_AddDay.Text);

                // Invert the numbers if the user is willing to subtract from the date instead
                if (DateAddSub_Addition.IsChecked == false)
                {
                    numYears  = -numYears;
                    numMonths = -numMonths;
                    numDays   = -numDays;
                }

                var date = new DateTime(year, month, day, cal);
                DateAddSub_Result.Text = DateCalculator.AddToDate(date, cal, numYears, numMonths, numDays);

                var result = DateCalculator.AddToDate(date, numYears, numMonths, numDays);
                SetToolTip(DateAddSub_Result, DateCalculator.GetDateInfo(result, cal));
            }
            catch (Exception ex)
            {
                DateAddSub_Result.Text = Constants.ValuePlaceholder;
                SetToolTip(DateAddSub_Result, null);

                ShowMessagebox(ex.Message, ".لطفا تواریخ را به درستی وارد کنید");
            }
        }

        private void DateAddSub_CopyResult_Click(object sender, RoutedEventArgs e)
        {
            if (DateAddSub_Result.Text != Constants.ValuePlaceholder)
                Clipboard.SetText(DateAddSub_Result.Text);
        }

        private void TimeAddSub_Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int hour1   = int.Parse(TimeAddSub_HourFrom.Text);
                int hour2   = int.Parse(TimeAddSub_HourTo.Text);
                int minute1 = int.Parse(TimeAddSub_MinuteFrom.Text);
                int minute2 = int.Parse(TimeAddSub_MinuteTo.Text);
                int second1 = int.Parse(TimeAddSub_SecondFrom.Text);
                int second2 = int.Parse(TimeAddSub_SecondTo.Text);

                // Used to detect whether to perform calculations for a normal TimeSpan or for a clock-like one
                if (TimeAddSub_UseClockMode.IsChecked == true)
                {
                    // Negate the values in order to subtract from the time if specified by the user
                    if (TimeAddSub_Addition.IsChecked == false)
                    {
                        hour2   = -hour2;
                        minute2 = -minute2;
                        second2 = -second2;
                    }

                    var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour1, minute1, second1);
                    TimeAddSub_Result.Text = TimeCalculator.AddTime(date, hour2, minute2, second2).ToString("HH:mm:ss");
                }
                else
                {
                    var time1 = new TimeSpan(hour1, minute1, second1);
                    var time2 = new TimeSpan(hour2, minute2, second2);

                    if (TimeAddSub_Addition.IsChecked == false)
                        time2 = -time2;

                    TimeAddSub_Result.Text = TimeCalculator.AddTime(time1, time2).ToString();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                TimeAddSub_Result.Text = Constants.ValuePlaceholder;
                ShowMessagebox(ex.Message, ".مقدار اولین فیلد ساعت باید بین 0 و 23 باشد");
            }
            catch (Exception ex)
            {
                TimeAddSub_Result.Text = Constants.ValuePlaceholder;
                ShowMessagebox(ex.Message, ".لطفا زمان‌ها را به درستی وارد کنید");
            }
        }

        private void TimeAddSub_CopyResult_Click(object sender, RoutedEventArgs e)
        {
            if (TimeAddSub_Result.Text != Constants.ValuePlaceholder)
                Clipboard.SetText(TimeAddSub_Result.Text);
        }

        private void About_GithubLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => System.Diagnostics.Process.Start(About_GithubLink.Text);

        private void SetToolTip(TextBlock tb, DateInfo info)
            => tb.ToolTip = $"{info.DayOfWeek}، {info.DayOfMonth} {info.MonthName} {info.Year}";

        private void SetToolTip(TextBlock tb, string value) => tb.ToolTip = value;

        private void ShowMessagebox(string exMessage, string hint)
            => MessageBox.Show($":خطایی رخ داد\n{exMessage}\n\n{hint}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RightAlign);
    }
}
