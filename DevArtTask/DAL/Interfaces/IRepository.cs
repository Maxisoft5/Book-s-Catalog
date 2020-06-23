
namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Read();
        void Insert();
        void Delete();
        void Update();
    }
}
