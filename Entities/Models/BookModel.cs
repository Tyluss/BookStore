namespace BookStore.Entities.Models
{
    public class BookModel
    {
        public long id { get; set; }

        public string? name { get; set; }

        public string? author { get; set; }

        public string? genre { get; set; }

        public double? price { get; set; }
    }
}
