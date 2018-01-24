namespace SIENN.DbAccess.Entity
{
    public class ProductToCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
    }
}