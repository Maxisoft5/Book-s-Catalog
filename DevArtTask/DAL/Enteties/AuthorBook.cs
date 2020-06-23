
using DAL.Enteties.Abstracions;

namespace DAL.Enteties
{
    public class AuthorBook : BaseEntity
    {

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
