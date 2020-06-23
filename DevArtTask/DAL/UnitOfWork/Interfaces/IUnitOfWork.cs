using DAL.Repositories;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BookRepository Books { get; }
        AuthorRepository Authors { get; }
        AuthorBookRepository AuthorBooks { get; }
    }
}
