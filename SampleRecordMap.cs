using System;
using CsvHelper.Configuration;
using TimeZoneConverter;
namespace normalizer
{
    public class SampleRecordMap : ClassMap<SampleRecord>
    {
        public SampleRecordMap()
        {
            AutoMap(System.Globalization.CultureInfo.InvariantCulture);
            TimeZoneInfo ptZone = TZConvert.GetTimeZoneInfo("Pacific Standard Time"); //TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            TimeSpan ptUtcOffset = ptZone.GetUtcOffset(DateTimeOffset.Now);
            TimeSpan localUtcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now);
            Map(m => m.Timestamp).Convert(row => 
                 /* add hours based on utc offset difference between local time (which was assumed on input) and US/Pacific (which was specified timezone for times missing timezones) */
                 /* slightly better here would be to consider some timestamp values may contain explicit timezone info */
                DateTime.Parse(row.Row.GetField("Timestamp"), System.Globalization.CultureInfo.InvariantCulture).AddHours(localUtcOffset.Subtract(ptUtcOffset).Hours)
                );
            Map(m => m.ZIP).Convert(row => row.Row.GetField("ZIP").PadLeft(5, '0'));
            Map(m => m.FullName).Convert(row => row.Row.GetField("FullName").ToUpper());
            Map(m => m.FooDuration).Ignore();//.Convert(row => TimeSpan.Parse(row.Row.GetField("FooDuration"), System.Globalization.CultureInfo.InvariantCulture));
            Map(m => m.BarDuration).Convert(row => TimeSpan.Parse(row.Row.GetField("BarDuration"), System.Globalization.CultureInfo.InvariantCulture));
            Map(m => m.TotalDuration).Ignore(); // ignore input
        }
    }
}