using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public class ProductUnit : BaseEntity
    {
        public ProductUnit()
        {
            Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}