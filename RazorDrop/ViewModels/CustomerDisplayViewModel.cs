using System;
using System.ComponentModel.DataAnnotations;

namespace RazorDrop.ViewModels
{
    public interface ICustomerDisplayViewModel
    {
        string CountryName { get; set; }
        Guid CustomerId { get; set; }
        string CustomerName { get; set; }
        string RegionName { get; set; }
    }

    public class CustomerDisplayViewModel : ICustomerDisplayViewModel
    {
        [Display(Name = "Customer Number")]
        public Guid CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "State / Province / Region")]
        public string RegionName { get; set; }
    }
}
