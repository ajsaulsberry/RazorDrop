using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorDrop.Models
{
    public class Country
    {
        [Key]
        [MaxLength(2)]
        public string CountryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CountryNameEnglish { get; set; }

        public List<Region> Regions { get; set; }
    }
}
