using Microsoft.EntityFrameworkCore;

namespace BookStore.Entities.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }
        public DbSet<BookModel>? Books { get; set; } = null;
    }
}
