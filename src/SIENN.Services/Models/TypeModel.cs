using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Models
{
    public class TypeModel : TypeBaseModel
    {
        public int Id { get; set; }
    }

    public class TypeBaseModel
    {

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}