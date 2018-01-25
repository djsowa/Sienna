using System.ComponentModel.DataAnnotations;

namespace SIENN.Services.Models
{
    public class UnitModel : UnitBaseModel
    {
        public int Id { get; set; }
    }

    public class UnitBaseModel
    {

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}