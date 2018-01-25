using System.Collections.Generic;
using System.Collections;
using SIENN.DbAccess.Entity;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Models
{
    public class ProductModel : ProductBaseModel
    {
        public ProductModel() : base()
        {
        }

        public int Id { get; set; }        
    }


    public class ProductBaseModel
    {
        public ProductBaseModel()
        {
            Categories = new List<int>();
        }

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

        [Required]
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }       
    }
}