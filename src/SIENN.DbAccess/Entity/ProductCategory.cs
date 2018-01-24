using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public class ProductCategory: BaseEntity
    {
        public ProductCategory()
        {
            
        }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<ProductToCategory> Products { get; } = new List<ProductToCategory>();

    }
}