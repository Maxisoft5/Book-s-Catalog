using DAL.Enteties.Abstracions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Author : BaseEntity
    {
        [Required(ErrorMessage = "Enter author's firstname"), MaxLength(45)]
        [RegularExpression(@"(^\w)|(\s\w)")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Enter author's lastname"), MaxLength(45)]
        [RegularExpression(@"(^\w)|(\s\w)")]
        public string Lirstname { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
