using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RazorDrop.Data
{
    public interface ICountriesRepository
    {
        public Task<IEnumerable<SelectListItem>> GetCountries();
    }
    public class CountriesRepository : ICountriesRepository
    {
        private readonly RazorDropContext _context;
        public CountriesRepository(RazorDropContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            List<SelectListItem> countries = await _context.Countries.AsNoTracking()
                .OrderBy(n => n.CountryNameEnglish)
                .Select(n =>
                    new SelectListItem
                    {
                        Value = n.CountryId.ToString(),
                        Text = n.CountryNameEnglish
                    }).ToListAsync();
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
