using System.Collections;
using SIENN.DbAccess.Entity;
using System.Linq;
using System;

namespace SIENN.Services.Models
{
    public class ProductModel : ProductBaseModel
    {
        public ProductModel() : base()
        {
        }

        public int Id { get; set; }
    }

    public class ProductViewModel
    {
        public ProductViewModel(ProductModel product, string typeCode, string typeDescription, string unitCode, string unitDescription)
        {
            this.ProductDescription = $"({product.Code}) {product.Description}";

            this.Price = $"{product.Price.ToString("N2")} zł";

            this.IsAvailable = product.IsAvailable;
            this.DeliveryDate = product.NextDelivery;
            this.CategoriesCount = product.Categories.Count;

            this.Unit = $"({unitCode}) {unitDescription}";
            this.Type = $"({typeCode}) {typeDescription}";

        }

        public string ProductDescription { get; set; }
        public string Price { get; set; }

        public bool IsAvailable { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public int CategoriesCount { get; set; }

        public string Type { get; set; }

        public string Unit { get; set; }
    }
}