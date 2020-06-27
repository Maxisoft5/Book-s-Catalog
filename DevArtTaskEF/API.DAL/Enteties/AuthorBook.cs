using API.DAL.Enteties.Abstracions;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DAL.Enteties
{
    public class AuthorBook : BaseEntity
    {
        [ForeignKey(nameof(Enteties.Author))]
        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }
        [ForeignKey(nameof(Enteties.Book))]
        public string BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
