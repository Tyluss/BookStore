namespace BookStore.Entities.Dtos
{
    public class BookDto
    {
        public long id { get; set; }

        public string? name { get; set; }

        public string? author { get; set; }

        public string? genre { get; set; }

        public double ? price { get; set; }
    }
}
