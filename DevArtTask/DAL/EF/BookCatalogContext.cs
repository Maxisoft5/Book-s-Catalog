using DAL.Enteties;
using System.Data.Entity;

namespace DAL.EF
{
    public class BookCatalogContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        public BookCatalogContext() : base("ConnectionString")
        {
        }
        public BookCatalogContext(string connectionString)
           : base(connectionString)
        {
        }

    }
}
