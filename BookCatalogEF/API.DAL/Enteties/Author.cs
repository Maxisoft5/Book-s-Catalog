using API.DAL.Enteties.Abstracions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DAL.Enteties
{
    public class Author : BaseEntity
    {
        [Required(ErrorMessage = "Enter author's firstname"), MaxLength(45)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Enter author's lastname"), MaxLength(45)]
        public string Lastname { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
