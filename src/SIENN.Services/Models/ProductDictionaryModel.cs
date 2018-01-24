namespace SIENN.Services.Models
{
    public class ProductDictionaryModel
    {

        public ProductDictionaryModel(int id, string code, string description)
        {
            Id = id;
            Code = code;
            Description = description;
        }
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}