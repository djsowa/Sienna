using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Models
{
    public class CategoryBaseModel
    {
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }

    public class CategoryModel : CategoryBaseModel
    {
        public int Id { get; set; }       
    }
}