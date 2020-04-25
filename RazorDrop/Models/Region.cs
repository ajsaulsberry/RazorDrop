using System.ComponentModel.DataAnnotations;

namespace RazorDrop.Models
{
    public class Region
    {
        [Key]
        [MaxLength(2)]
        public string RegionId { get; set; }

        [Required]
        public string RegionNameEnglish { get; set; }

        [Required]
        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
}
