namespace DateTimeCalculator
{
    public struct DateResults
    {
        public string ResultPersian { get; }
        public string ResultHijri { get; }
        public string ResultGregorian { get; }

        public DateResults(string resultPersian, string resultHijri, string resultGregorian)
        {
            ResultPersian   = resultPersian;
            ResultHijri     = resultHijri;
            ResultGregorian = resultGregorian;
        }
    }
}
