using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using WebApiContrib.Core.Formatter.Csv;

namespace WebApiContrib.Core.Csv.Tests
{
    public class CsvTestsHelper
    {
        public static CsvFormatterOptions Options = new CsvFormatterOptions
        {
            CsvDelimiter = ";",
            EscapeQuote = "\"\"",
            QuoteCharacter = "\"",
            UseSingleLineHeaderInCsv = true,
        };

        public static CsvReader CreateCsvReader<T>(StreamReader reader) where T : ClassMap
        {
            var csvParser = new CsvReader(reader);
            csvParser.Configuration.RegisterClassMap<T>();
            csvParser.Configuration.Delimiter = Options.CsvDelimiter;
            csvParser.Configuration.IgnoreQuotes = true;

            return csvParser;
        }

        public static string UnescapeQuote(string field)
        {
            if (field.StartsWith("\""))
                field = field.Substring(1);

            if (field.EndsWith(Options.EscapeQuote) && field.EndsWith(Options.QuoteCharacter))
                field = field.Substring(0, field.Length - 1);

            return field.Replace(Options.EscapeQuote, Options.QuoteCharacter);
        }
    }
}
