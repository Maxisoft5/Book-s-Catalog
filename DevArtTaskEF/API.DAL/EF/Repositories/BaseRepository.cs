using API.DAL.EF.Interfaces;
using API.DAL.Enteties;
using DAL.EF;
using Devart.Data.SQLite;
using NLog;
using System;
using System.Linq;

namespace API.DAL.EF.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        public void Delete()
        {
            bool IsDigit = true;
            while (IsDigit)
            {
                Console.WriteLine("Enter a book's id");
                string id = Console.ReadLine();
                id.ToCharArray();
                if (Char.IsDigit(id.First()))
                {
                    using (BookCatalogContext context = new BookCatalogContext())
                    {
                        try
                        {
                            Book book = context.Books.First(b => b.Id == id);
                            context.Books.Remove(book);
                            context.SaveChanges();
                        }
                        catch (NullReferenceException ex)
                        {
                            logger.Trace($"{ex.Message}. Error encountered during DELETE operation. - {DateTime.Now}");
                            Console.WriteLine($"Error encountered during DELETE operation. See details in log file");
                        }
                        catch (SQLiteException ex)
                        {
                            logger.Trace($"{ex.Message}. Error encountered during DELETE operation. - {DateTime.Now}");
                            Console.WriteLine($"Error encountered during DELETE operation. See details in log file");
                        }
                        catch (Exception ex)
                        {
                            logger.Trace($"{ex.Message}.Error encountered during DELETE operation. - {DateTime.Now}");
                            Console.WriteLine($"{ex.Message}.Error encountered during DELETE operation. See details in log file");
                        }
                        finally
                        {
                            logger.Trace($"DELETE operation successfully completed at {DateTime.Now};");
                            IsDigit = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Enter an id");
                }
            }
        }
        public void Insert()
        {
            string firstname = "";
            string lastname = "";
            bool isNotselected = true;
            while (isNotselected)
            {
                Console.WriteLine("Choose books' author: 1 - Nassim Taleb, 2 - Gerbert Shield, 3 - Alexandr Pushkin, 4 - Book doesn't have author");
                string ch = Console.ReadLine();
                char[] charArr = ch.ToCharArray();
                char key = charArr[0];
                if (Char.IsDigit(key))
                {
                    switch (charArr[0])
                    {
                        case '1':
                            firstname = "Nassim";
                            lastname = "Taleb";
                            isNotselected = false;
                            break;
                        case '2':
                            firstname = "Gerbert";
                            lastname = "Shield";
                            isNotselected = false;
                            break;
                        case '3':
                            firstname = "Alexandr";
                            lastname = "Pushkin";
                            isNotselected = false;
                            break;
                        case '4':
                            firstname = "";
                            lastname = "";
                            isNotselected = false;
                            break;
                    }
                }
            }
            Console.WriteLine("Enter a books's name");
            string bookName = Console.ReadLine();
            Console.WriteLine("Enter a books's count");
            string countinstance = Console.ReadLine();
            Console.WriteLine("Enter a books's price");
            string price = Console.ReadLine();
            Console.WriteLine("Enter a books's genre");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter a books's assessments");
            string assessments = Console.ReadLine();
            using (BookCatalogContext context = new BookCatalogContext())
            {
                try
                {
                    Author author = new Author
                    {
                        Firstname = firstname,
                        Lastname = lastname
                    };
                    Book book = new Book
                    {
                        Bookname = bookName,
                        Assessmnets = Convert.ToInt32(assessments),
                        Countinstance = Convert.ToInt32(countinstance),
                        Genre = genre,
                        Price = Convert.ToInt32(price)
                    };
                    AuthorBook authorBook = new AuthorBook
                    {
                        Author = author,
                        Book = book,
                        AuthorId = author.Id,
                        BookId = book.Id
                    };
                    context.Authors.Add(author);
                    context.Books.Add(book);
                    context.AuthorBooks.Add(authorBook);
                    context.SaveChanges();
                }
                catch (NullReferenceException ex)
                {
                    logger.Trace($"{ex.Message}. Error encountered during INSERT operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during INSERT operation. See details in log file");
                }
                catch (SQLiteException ex)
                {
                    logger.Trace($"{ex.Message}. Error encountered during INSERT operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during INSERT operation. See details in log file");
                }
                catch (Exception ex)
                {
                    logger.Trace($"{ex.Message}.Error encountered during INSERT operation. - {DateTime.Now}");
                    Console.WriteLine($"{ex.Message}.Error encountered during INSERT operation. See details in log file");
                }
                finally
                {
                    logger.Trace($"READING operation ended at {DateTime.Now};");
                }
            }
        }

        public void Read()
        {
            using (BookCatalogContext context = new BookCatalogContext())
            {
                try
                {
                    Console.WriteLine("Books without authors:");
                    var books = from b in context.Books
                                join ab in context.AuthorBooks on b.Id equals ab.Id
                                where ab.AuthorId == null
                                select new
                                {
                                    Bookname = b.Bookname,
                                    Countinstance = b.Countinstance,
                                    Price = b.Price,
                                    Genre = b.Genre,
                                    Assessmnets = b.Assessmnets,
                                };
                    Console.WriteLine("\t\tBooks without authors");
                    Console.Write(Environment.NewLine);
                    foreach (var b in books)
                    {
                        // printing the table content
                        Console.WriteLine(
                            " Bookname: {0}\n Countinstance: {1}\n Assessmnets: {2}\n Genre: {3}\n Price: {4}\n",
                            b.Bookname, b.Countinstance, b.Assessmnets, b.Genre, b.Price);
                        Console.WriteLine(new string('>', 40));
                        Console.Write(Environment.NewLine);
                    }
                    Console.WriteLine("Books with authors:");
                    var authorsBooks = from b in context.Books
                                       join ab in context.AuthorBooks on b.Id equals ab.Id
                                       select new
                                       {
                                           Id = b.Id,
                                           Bookname = b.Bookname,
                                           Countinstance = b.Countinstance,
                                           Price = b.Price,
                                           Genre = b.Genre,
                                           Assessmnets = b.Assessmnets,
                                       };
                    Console.WriteLine("\t\tBooks with authors");
                    Console.Write(Environment.NewLine);
                    foreach (var b in authorsBooks)
                    {
                        // printing the table content
                        Console.WriteLine(
                            " Id:{0}\n Bookname: {1}\n Countinstance: {2}\n Assessmnets: {3}\n Genre: {4}\n Price: {5}\n",
                            b.Id, b.Bookname, b.Countinstance, b.Assessmnets, b.Genre, b.Price);
                        Console.WriteLine(new string('>', 40));
                        Console.Write(Environment.NewLine);
                    }
                }
                catch (NullReferenceException ex)
                {
                    logger.Trace($"{ex.Message}. Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during READING operation. See details in log file");
                }
                catch (SQLiteException ex)
                {
                    logger.Trace($"{ex.Message}. Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"Error encountered during READING operation. See details in log file");
                }
                catch (Exception ex)
                {
                    logger.Trace($"{ex.Message}.Error encountered during READING operation. - {DateTime.Now}");
                    Console.WriteLine($"{ex.Message}.Error encountered during READING operation. See details in log file");
                }
                finally
                {
                    logger.Trace($"READING operation ended at {DateTime.Now};");
                }
            }
        }

        public void Update()
        {
            bool IsDigit = true;
            while (IsDigit)
            {
                Console.WriteLine("Enter a book's id");
                string id = Console.ReadLine();
                id.ToCharArray();
                if (Char.IsDigit(id.First()))
                {
                    string firstname = "";
                    string lastname = "";
                    bool isNotselected = true;
                    while (isNotselected)
                    {
                        Console.WriteLine("Choose books' author: 1 - Nassim Taleb, 2 - Gerbert Shield, 3 - Alexandr Pushkin, 4 - Book doesn't have author");
                        string ch = Console.ReadLine();
                        char[] charArr = ch.ToCharArray();
                        char key = charArr[0];
                        if (Char.IsDigit(key))
                        {
                            switch (charArr[0])
                            {
                                case '1':
                                    firstname = "Nassim";
                                    lastname = "Taleb";
                                    isNotselected = false;
                                    break;
                                case '2':
                                    firstname = "Gerbert";
                                    lastname = "Shield";
                                    isNotselected = false;
                                    break;
                                case '3':
                                    firstname = "Alexandr";
                                    lastname = "Pushkin";
                                    isNotselected = false;
                                    break;
                                case '4':
                                    firstname = "";
                                    lastname = "";
                                    isNotselected = false;
                                    break;
                            }
                        }
                    }
                    Console.WriteLine("Enter a books's name");
                    string bookName = Console.ReadLine();
                    Console.WriteLine("Enter a books's count");
                    string countinstance = Console.ReadLine();
                    Console.WriteLine("Enter a books's price");
                    string price = Console.ReadLine();
                    Console.WriteLine("Enter a books's genre");
                    string genre = Console.ReadLine();
                    Console.WriteLine("Enter a books's assessments");
                    string assessments = Console.ReadLine();
                    using (BookCatalogContext context = new BookCatalogContext())
                    {
                        Book book = context.Books.Where(a => a.Id == id).First();
                        book.Bookname = bookName;
                        book.Countinstance = Convert.ToInt32(countinstance);
                        book.Assessmnets = Convert.ToInt32(assessments);
                        book.Genre = genre;
                        book.Price = Convert.ToInt32(price);
                        context.SaveChanges();
                        AuthorBook author = context.AuthorBooks.Where(a => a.BookId == id).First();
                        if ((firstname != "") && (lastname != ""))
                        {
                            author.Author.Firstname = firstname;
                            author.Author.Lastname = lastname;
                            context.SaveChanges();
                        }
                        else
                        {
                            author.Author = null;
                            author.AuthorId = null;
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
