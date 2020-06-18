using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorDrop.ViewModels
{
    public interface ICustomerEditViewModel
    {
        IEnumerable<SelectListItem> Countries { get; set; }
        string CustomerId { get; set; }
        string CustomerName { get; set; }
        IEnumerable<SelectListItem> Regions { get; set; }
        string SelectedCountryId { get; set; }
        string SelectedRegionId { get; set; }
    }

    public class CustomerEditViewModel : ICustomerEditViewModel
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
