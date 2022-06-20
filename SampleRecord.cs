using System;

namespace normalizer
{
    public class SampleRecord
    {
        public DateTime Timestamp { get; set; }
        public string Address { get; set; }
        public string ZIP { get; set; }
        public string FullName { get; set; }
        public TimeSpan FooDuration { get; set; }
        public TimeSpan BarDuration { get; set; }
        public TimeSpan TotalDuration { get { return FooDuration + BarDuration; } }
        public string Notes { get; set; }
    }
}