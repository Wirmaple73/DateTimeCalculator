using System.Globalization;

namespace DateTimeCalculator
{
    public static class Constants
    {
        public const string ValuePlaceholder = "~";

        public static readonly PersianCalendar PersianCalendar = new PersianCalendar();
        public static readonly UmAlQuraCalendar HijriCalendar = new UmAlQuraCalendar();
        public static readonly GregorianCalendar GregorianCalendar = new GregorianCalendar();

        public static readonly string[] Days = { "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه", "شنبه" };

        public static readonly string[] PersianMonths   = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        public static readonly string[] HijriMonths     = { "محرم", "صفر", "ربیع الاول", "ربیع الثانی", "جمادی الاول", "جمادی الثانی", "رجب", "شعبان", "رمضان", "شوال", "ذی‌قعده", "ذی‌حجه" };
        public static readonly string[] GregorianMonths = { "ژانویه", "فوریه", "مارس", "آوریل", "می", "ژوئن", "جولای", "اوت", "سپتامبر", "اکتبر", "نوامبر", "دسامبر" };
    }
}
