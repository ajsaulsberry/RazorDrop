using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public ICustomerEditViewModel CustomerEditViewModel { get; set; }

        public CreateModel(ICustomersRepository customersRepo, IRegionsRepository regionsRepo)
        {
            _customersRepo = customersRepo;
            _regionsRepo = regionsRepo;
        }
        public IActionResult OnGet()
        {
            CustomerEditViewModel = _customersRepo.CreateCustomer();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool saved = _customersRepo.SaveCustomer(CustomerEditViewModel);
                    if (saved)
                    {
                        return RedirectToAction("Index");
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

        public IActionResult OnPostRegions()
        {
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyToAsync(stream);
            stream.Position = 0;
            using StreamReader reader = new StreamReader(stream);
            string requestBody = reader.ReadToEnd();
            if (requestBody.Length > 0)
            {
                IEnumerable<SelectListItem> regions = _regionsRepo.GetRegions(requestBody);
                return new JsonResult(regions);
            }
            return null;
        }
    }
}
