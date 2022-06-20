using System;
using System.IO;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace normalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(Console.OpenStandardInput()))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<SampleRecordMap>();
                var records = csv.GetRecords<SampleRecord>();
                using (var writer = new StreamWriter(Console.OpenStandardOutput()))  // UTF-8 is default streamwriter output encoding
                using (var csvout = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    var dateFormatOptions = new TypeConverterOptions { Formats = new[] { "o" } }; // "o" corresponds to "round-trip" style (an ISO8601 representation which I believe is also RFC3339-compliant) 
                    csvout.Context.TypeConverterOptionsCache.AddOptions<DateTime>(dateFormatOptions);
                    csvout.WriteRecords(records);
                }
            }
        }
    }
}