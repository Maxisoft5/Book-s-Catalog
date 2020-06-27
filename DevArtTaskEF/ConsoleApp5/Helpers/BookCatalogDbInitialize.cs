using DAL.EF;
using DAL.Enteties;
using System.Collections.Generic;
using System.Data.Entity;

namespace DAL.Helpers
{
    public class BookCatalogDbInitialize : DropCreateDatabaseAlways<BookCatalogContext>
    {
       // add db
        protected override void Seed(BookCatalogContext context)
        {
            var author1 = new Author
            {
                Firstname = "Nassim",
                Lastname = "Taleb"
            };
            var author2 = new Author
            {
                Firstname = "Gerbert",
                Lastname = "Shield"
            };
            var author3 = new Author
            {
                Firstname = "Alexandr",
                Lastname = "Pushkin"
            };
            context.Authors.AddRange(new List<Author> { author1, author2, author3 });
            context.SaveChanges();

            var book1 = new Book
            {
                Bookname = "Anti-fragle",
                Countinstance = 24,
                Genre = "Finance",
                Assessmnets = 5,
                Price = 240
            };
            var book2 = new Book
            {
                Bookname = "How to read books",
                Countinstance = 10,
                Genre = "Phylosophy",
                Assessmnets = 4,
                Price = 120
            };
            var book3 = new Book
            {
                Bookname = "C# 8.0",
                Countinstance = 14,
                Genre = "Technical literature",
                Assessmnets = 4,
                Price = 320
            };
            context.Books.AddRange(new List<Book> { book1, book2, book3 });
            context.SaveChanges();

            var bookAuthor1 = new AuthorBook
            {
                Author = author1,
                AuthorId = author1.Id,
                Book = book1,
                BookId = book1.Id
            };
            var bookAuthor2 = new AuthorBook
            {
                Book = book2,
                BookId = book2.Id,
                Author = null,
                AuthorId = null
            };
            var bookAuthor3 = new AuthorBook
            {
                Book = book3,
                BookId = book3.Id,
                Author = author2,
                AuthorId = author2.Id
            };
            context.AuthorBooks.AddRange(new List<AuthorBook> { bookAuthor1, bookAuthor2, bookAuthor3 });
            context.SaveChanges();
        }
    }
}
