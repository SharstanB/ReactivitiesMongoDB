using Domain.Absractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public interface IBaseEntity : ISoftDeletable
    {
        [Key]
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; } 

    }
    public abstract class BaseEntity  : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

       

    }

}
