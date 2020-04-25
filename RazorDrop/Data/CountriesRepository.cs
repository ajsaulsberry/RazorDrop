using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RazorDrop.Data
{
    public class CountriesRepository
    {
        private readonly RazorDropContext _context;
        public CountriesRepository(RazorDropContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = _context.Countries.AsNoTracking()
                .OrderBy(n => n.CountryNameEnglish)
                .Select(n =>
                    new SelectListItem
                    {
                        Value = n.CountryId.ToString(),
                        Text = n.CountryNameEnglish
                    }).ToList();
            var countrytip = new SelectListItem()
            {
                Value = null,
                Text = "--- select country ---"
            };
            countries.Insert(0, countrytip);
            return new SelectList(countries, "Value", "Text");
        }
    }
}
