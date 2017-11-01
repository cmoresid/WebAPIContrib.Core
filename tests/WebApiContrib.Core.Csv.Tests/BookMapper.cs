using CsvHelper.Configuration;

namespace WebApiContrib.Core.Csv.Tests
{
    public sealed class BookMapper : ClassMap<Book>
    {
        public BookMapper()
        {
            Map(book => book.Title).Name("Title");
            Map(book => book.Author).Name("Author");
        }
    }
}
