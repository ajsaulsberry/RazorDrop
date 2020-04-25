using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDrop.Data;
using RazorDrop.ViewModels;

namespace RazorDrop.Pages.Customers
{
    public class IndexModel : PageModel
    {

        private readonly RazorDropContext _context;

        [BindProperty(SupportsGet = true)]
        public List<CustomerDisplayViewModel> CustomersDisplayList { get; set; }

        public IndexModel(RazorDropContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var repo = new CustomersRepository(_context);
            CustomersDisplayList = repo.GetCustomers();
            return Page();
        }
    }
}
