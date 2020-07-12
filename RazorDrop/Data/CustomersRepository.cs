using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorDrop.Models;
using RazorDrop.ViewModels;

namespace RazorDrop.Data
{
    public interface ICustomersRepository
    {
        public Task<List<CustomerDisplayViewModel>> GetCustomers();
        public Task<CustomerEditViewModel> CreateCustomer();
        public Task<bool> SaveCustomer(CustomerEditViewModel customeredit);
    }

    public class CustomersRepository : ICustomersRepository
    {
        private readonly RazorDropContext _context;
        private readonly ICountriesRepository _countriesRepo;
        private readonly IRegionsRepository _regionsRepo;
        public CustomersRepository(RazorDropContext context, ICountriesRepository countriesRepo, IRegionsRepository regionsRepo)
        {
            _context = context;
            _countriesRepo = countriesRepo;
            _regionsRepo = regionsRepo;
        }

        public async Task<List<CustomerDisplayViewModel>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers = await _context.Customers.AsNoTracking()
                .Include(x => x.Country)
                .Include(x => x.Region)
                .ToListAsync();

            if (customers != null)
            {
                List<CustomerDisplayViewModel> customersDisplay = new List<CustomerDisplayViewModel>();
                foreach (var x in customers)
                {
                    var customerDisplay = new CustomerDisplayViewModel()
                    {
                        CustomerId = x.CustomerId,
                        CustomerName = x.CustomerName,
                        CountryName = x.Country.CountryNameEnglish,
                        RegionName = x.Region.RegionNameEnglish
                    };
                    customersDisplay.Add(customerDisplay);
                }
                return customersDisplay;
            }
            return null;
        }

        public async Task<CustomerEditViewModel> CreateCustomer()
        {
            var customer = new CustomerEditViewModel()
            {
                CustomerId = Guid.NewGuid().ToString(),
                Countries = await _countriesRepo.GetCountries(),
                Regions = _regionsRepo.GetRegions()
            };
            return customer;
        }

        public async Task<bool> SaveCustomer(CustomerEditViewModel customeredit)
        {
            if (customeredit != null)
            {
                if (Guid.TryParse(customeredit.CustomerId, out Guid newGuid))
                {
                    var customer = new Customer()
                    {
                        CustomerId = newGuid,
                        CustomerName = customeredit.CustomerName,
                        CountryId = customeredit.SelectedCountryId,
                        RegionId = customeredit.SelectedRegionId
                    };
                    // The next two lines will execute sequentially.
                    customer.Country = await _context.Countries.FindAsync(customeredit.SelectedCountryId);
                    customer.Region = await _context.Regions.FindAsync(customeredit.SelectedRegionId);

                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            // Return false if customeredit == null or CustomerID is not a Guid.
            return false;
        }
    }
}
