using System;
using System.ComponentModel.DataAnnotations;

namespace RazorDrop.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(128)]
        public string CustomerName { get; set; }

        [Required]
        public string CountryId { get; set; }
        public Country Country { get; set; }

        public string RegionId { get; set; }
        public Region Region { get; set; }
    }
}
