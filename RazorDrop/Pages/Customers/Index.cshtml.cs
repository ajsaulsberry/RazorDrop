using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDrop.Data;
using RazorDrop.ViewModels;

namespace RazorDrop.Pages.Customers
{
    public class IndexModel : PageModel
    {

        private readonly ICustomersRepository _customersRepo;

        [BindProperty(SupportsGet = true)]
        public List<ICustomerDisplayViewModel> CustomersDisplayList { get; set; }

        public IndexModel(ICustomersRepository customersRepo)
        {
            _customersRepo = customersRepo;
        }

        public IActionResult OnGet()
        {
            CustomersDisplayList = _customersRepo.GetCustomers();
            return Page();
        }
    }
}
