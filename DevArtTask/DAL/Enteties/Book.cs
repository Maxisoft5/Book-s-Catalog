using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Enteties
{
    public class Book
    {
        public int Id { get; set; }
        [Required, MaxLength(45)]
        public string Bookname { get; set; }
        public int Countinstance { get; set; }
        public decimal Price { get; set; }
        [Required, MaxLength(30)]
        public string Genre { get; set; }
        public int Assessmnets { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
