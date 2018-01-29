using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Models
{
    public class ProductFilterModel
    {
        public ProductFilterModel()
        {
            Categories = new List<int>();
        }
        public virtual List<int> Categories { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> ProductTypeId { get; set; }

        [Required]
        public int Skip { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Range(5, 50)]
        public int Take { get; set; }
    }
}