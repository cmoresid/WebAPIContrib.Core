using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace WebApiContrib.Core.Csv.Tests
{
    // note: the JSON tests are here to verify that the two formatters do not conflict with each other
    public class CsvTests
    {
        private TestServer _server;

        public CsvTests()
        {
            _server = new TestServer(new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>());
        }

        [Fact]
        public async Task GetCollection_Text_Csv_Header()
        {
            var client = _server.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/books");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));

            var result = await client.SendAsync(request);

            using (var streamReader = new StreamReader(await result.Content.ReadAsStreamAsync()))
            using (var csvReader = CsvTestsHelper.CreateCsvReader<BookMapper>(streamReader))
            {
                var results = csvReader.GetRecords<Book>().ToList();

                Assert.Equal(Book.Data.Length, results.Count);

                for (var i = 0; i < Book.Data.Length; i++)
                {
                    var x = CsvTestsHelper.UnescapeQuote(results[i].Title);

                    Assert.Equal(
                        Book.Data[i].Author,
                        CsvTestsHelper.UnescapeQuote(results[i].Author)
                    );

                    Assert.Equal(
                        Book.Data[i].Title,
                        CsvTestsHelper.UnescapeQuote(results[i].Title)
                    );
                }
            }
        }

        [Fact]
        public async Task GetCollection_JSON_Header()
        {
            var client = _server.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/books");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.SendAsync(request);
            var books = JsonConvert.DeserializeObject<Book[]>(await result.Content.ReadAsStringAsync());

            Assert.Equal(3, books.Length);
            Assert.Equal(Book.Data[0].Author, books[0].Author);
            Assert.Equal(Book.Data[0].Title, books[0].Title);
            Assert.Equal(Book.Data[1].Author, books[1].Author);
            Assert.Equal(Book.Data[1].Title, books[1].Title);
            Assert.Equal(Book.Data[2].Author, books[2].Author);
            Assert.Equal(Book.Data[2].Title, books[2].Title);
        }
    }
}
