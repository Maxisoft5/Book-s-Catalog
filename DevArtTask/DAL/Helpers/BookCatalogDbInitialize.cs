using DAL.EF;
using DAL.Enteties;
using System.Collections.Generic;
using System.Data.Entity;

namespace DAL.Helpers
{
    public class BookCatalogDbInitialize : DropCreateDatabaseAlways<BookCatalogContext>
    {
        //add db 
        protected override void Seed(BookCatalogContext db)
        {
            var author1 = new Author
            {
                Id = 1,
                Firstname = "Nassim",
                Lirstname = "Taleb"
            };
            var author2 = new Author
            {
                Id = 2,
                Firstname = "Gerbert",
                Lirstname = "Shield"
            };
            var author3 = new Author
            {
                Id = 1,
                Firstname = "Alexandr",
                Lirstname = "Pushkin"
            };
            db.Authors.AddRange(new List<Author> { author1, author2, author3 });

            var book1 = new Book
            {
                Id = 1,
                Bookname = "Anti-fragle",
                Countinstance = 24,
                Genre = "Finance",
                Assessmnets = 5,
                Price = 240
            };
            var book2 = new Book
            {
                Id = 2,
                Bookname = "How to read books",
                Countinstance = 10,
                Genre = "Phylosophy",
                Assessmnets = 4,
                Price = 120
            };
            var book3 = new Book
            {
                Id = 3,
                Bookname = "C# 8.0",
                Countinstance = 14,
                Genre = "Technical literature",
                Assessmnets = 4,
                Price = 320
            };
            db.Books.AddRange(new List<Book> { book1, book2, book3 });

            var bookAuthor1 = new AuthorBook
            {
                Id = 1,
                Author = author1,
                AuthorId = author1.Id,
                Book = book1,
                BookId = book1.Id
            };
            var bookAuthor2 = new AuthorBook
            {
                Id = 1,
                Book = book2,
                BookId = book2.Id
            }; 
            var bookAuthor3= new AuthorBook
            {
                Id = 1,
                Book = book3,
                BookId = book3.Id,
                Author = author2,
                AuthorId = author2.Id
            };
            db.AuthorBooks.AddRange(new List<AuthorBook> { bookAuthor1, bookAuthor2, bookAuthor3 });
            db.SaveChanges();
        }
    }
}
