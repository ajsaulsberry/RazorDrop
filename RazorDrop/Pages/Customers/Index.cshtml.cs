using System.Collections.Generic;
using System.Threading.Tasks;
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
        public List<CustomerDisplayViewModel> CustomersDisplayList { get; set; }

        public IndexModel(ICustomersRepository customersRepo)
        {
            _customersRepo = customersRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            CustomersDisplayList = await _customersRepo.GetCustomers();
            return Page();
        }
    }
}
