using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorDrop.Data;
using RazorDrop.ViewModels;

namespace RazorDrop.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomersRepository _customersRepo;
        private readonly IRegionsRepository _regionsRepo;

        [BindProperty(SupportsGet = true)]
        public CustomerEditViewModel CustomerEditViewModel { get; set; }

        public CreateModel(ICustomersRepository customersRepo, IRegionsRepository regionsRepo)
        {
            _customersRepo = customersRepo;
            _regionsRepo = regionsRepo;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            CustomerEditViewModel = await _customersRepo.CreateCustomer();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool saved = await _customersRepo.SaveCustomer(CustomerEditViewModel);
                    if (saved)
                    {
                        return RedirectToPage("Index");
                    }
                }
                // Handling model state errors is beyond the scope of the demo, so just throwing an ApplicationException when the ModelState is invalid
                // and rethrowing it in the catch block.
                throw new ApplicationException("Invalid model");
            }
            catch (ApplicationException ex)
            {
                Debug.Write(ex.Message);
                throw ex;
            }
        }

        public async Task<IActionResult> OnPostRegionsAsync()
        {
            MemoryStream stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Position = 0;
            using StreamReader reader = new StreamReader(stream);
            string requestBody = reader.ReadToEnd();
            if (requestBody.Length > 0)
            {
                IEnumerable<SelectListItem> regions = await _regionsRepo.GetRegions(requestBody);
                return new JsonResult(regions);
            }
            return null;
        }
    }
}
