using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIENN.DbAccess.Entity
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Categories = new List<ProductToCategory>();
        }

        // public Product(List<ProductToCategory> categories) : this()
        // {
        //     categories.ForEach(c => { c.Product = this; c.ProductId = this.Id; Categories.Add(c); });
        // }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        public DateTime? NextDelivery { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }

        public virtual ICollection<ProductToCategory> Categories { get; set; } 

        [Required]
        public ProductUnit Unit { get; set; }
        public int UnitId { get; set; }
    }
}