using DAL.Enteties;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Author> Authors { get; }
        IRepository<Book> Books { get; }
        IRepository<AuthorBook> AuthorBooks { get; }
    }
}
