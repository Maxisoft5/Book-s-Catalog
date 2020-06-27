using System;
using System.ComponentModel.DataAnnotations;

namespace API.DAL.Enteties.Abstracions
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
