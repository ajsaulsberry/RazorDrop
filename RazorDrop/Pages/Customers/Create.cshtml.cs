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
        private readonly RazorDropContext _context;

        [BindProperty(SupportsGet = true)]
        public CustomerEditViewModel CustomerEditViewModel { get; set; }

        public CreateModel(RazorDropContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            var repo = new CustomersRepository(_context);
            CustomerEditViewModel = repo.CreateCustomer();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var repo = new CustomersRepository(_context);
                    bool saved = repo.SaveCustomer(CustomerEditViewModel);
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
                var repo = new RegionsRepository(_context);

                IEnumerable<SelectListItem> regions = repo.GetRegions(requestBody);
                return new JsonResult(regions);
            }
            return null;
        }
    }
}
