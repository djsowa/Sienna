using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SIENN.Services.Models
{
    public class ProductBaseModel
    {
        public ProductBaseModel()
        {
            Categories = new List<int>();
        }

        // public ProductBaseModel(string code, string description, int unitId, int productTypeId, decimal price, DateTime? nextDelivery, List<int> categories = null) : this()
        // {
        //     Code = code.Length > 20 ? code.Substring(0, 19) : code;
        //     Description = description.Length > 255 ? description.Substring(0, 254) : description;
        //     UnitId = unitId;
        //     ProductTypeId = productTypeId;
        //     Price = price;
        //     NextDelivery = nextDelivery;

        //     if (categories != null && categories.Any())
        //     {
        //         Categories.AddRange(categories);
        //     }
        // }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
        public virtual List<int> Categories { get; set; }

        [Required]
        public int UnitId { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime? NextDelivery { get; set; }

        [Required]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}