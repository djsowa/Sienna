using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public class Product
    {
        public Product()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        public ProductType ProductTypes { get; set; }

        public virtual ICollection<ProductToCategory> Categories { get; } = new List<ProductToCategory>();


        public ProductUnit Unit { get; set; }
    }
}