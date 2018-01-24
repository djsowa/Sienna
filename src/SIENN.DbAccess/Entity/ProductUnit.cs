using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public class ProductUnit
    {
        public ProductUnit()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}