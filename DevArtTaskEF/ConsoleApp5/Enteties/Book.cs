using DAL.Enteties.Abstracions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Book : BaseEntity
    {
        [Required(ErrorMessage = "Enter a book's name"), MaxLength(45)]
        public string Bookname { get; set; }
        [Required(ErrorMessage ="Enter a book's count")]
        public int Countinstance { get; set; }
        [Required(ErrorMessage = "Enter a book's price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter a book's genre"), MaxLength(30)]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Enter a book's assessments")]
        public int Assessmnets { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
