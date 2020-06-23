
namespace DAL.Repositories.Interfaces
{
    public interface IBookRepository<T> where T : class
    {
        void Read();
        void Insert();
        void Delete();
        void Update();
    }
}
