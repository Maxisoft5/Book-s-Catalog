using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;
using System;

namespace DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookCatalogContext _context;
        private AuthorRepository _authorRepository;
        private BookRepository _bookRepository;
        private AuthorBookRepository _authorBookRepository;
        private bool _disposed = false;
        public EFUnitOfWork(string connectionString)
        {
            _context = new BookCatalogContext(connectionString);
        }
        public BookRepository Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_context);
                return _bookRepository;
            }
        }
        public AuthorRepository Authors
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(_context);
                return _authorRepository;
            }
        }
        public AuthorBookRepository AuthorBooks
        {
            get
            {
                if (_authorBookRepository == null)
                    _authorBookRepository = new AuthorBookRepository(_context);
                return _authorBookRepository;
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
