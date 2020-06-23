using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Author
    {
        public int Id { get; set; }
        [Required, MaxLength(45)]
        [RegularExpression(@"(^\w)|(\s\w)")]
        public string Firstname { get;set; }
        [Required, MaxLength(45)]
        [RegularExpression(@"(^\w)|(\s\w)")]
        public string Lirstname { get; set; }
        [Required, MaxLength(45)]
        [RegularExpression(@"(^\w)|(\s\w)")]
        public string Middlename { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
