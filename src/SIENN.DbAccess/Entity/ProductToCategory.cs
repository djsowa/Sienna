using System.ComponentModel.DataAnnotations;

namespace SIENN.DbAccess.Entity
{
    public class ProductToCategory : BaseEntity
    {
        public ProductToCategory()
        {

        }

        public ProductToCategory(int productId, int categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }        

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
    }
}