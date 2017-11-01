namespace WebApiContrib.Core.Csv.Tests
{
    public class Book
    {
        public static Book[] Data = new[]
        {
            new Book { Title = "Our Mathematical Universe: My Quest for the Ultimate Nature of Reality", Author = "Max Tegmark"},
            new Book { Title = "Hockey Towns", Author = "Ron MacLean"},
            new Book { Title = "The \"Winter Man\"", Author = "Jill James"},
            new Book { Title = "Hello, \"World\"", Author = "Jimmy Bean" }
        };

        public string Title { get; set; }

        public string Author { get; set; }
    }
}
