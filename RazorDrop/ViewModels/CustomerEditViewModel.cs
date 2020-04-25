using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorDrop.ViewModels
{
    public class CustomerEditViewModel
    {
        [Display(Name = "Customer Number")]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(75)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string SelectedCountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [Display(Name = "State / Region")]
        public string SelectedRegionId { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }
    }
}
