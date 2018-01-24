using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}