namespace WebApiContrib.Core.Formatter.Csv
{
    public class CsvFormatterOptions
    {
        public bool UseSingleLineHeaderInCsv { get; set; } = true;

        public string CsvDelimiter { get; set; } = ";";

        public string QuoteCharacter { get; set; } = "\"";

        public string EscapeQuote { get; set; } = "\"\"";
    }
}
