using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}