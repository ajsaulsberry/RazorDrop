using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RazorDrop.Models;
using RazorDrop.ViewModels;

namespace RazorDrop.Data
{
    public interface ICustomersRepository
    {
        public IList<ICustomerDisplayViewModel> GetCustomers();
        public ICustomerEditViewModel CreateCustomer();
        public bool SaveCustomer(ICustomerEditViewModel customeredit);
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

        public IList<ICustomerDisplayViewModel> GetCustomers()
        {

            List<Customer> customers = new List<Customer>();
            customers = _context.Customers.AsNoTracking()
                .Include(x => x.Country)
                .Include(x => x.Region)
                .ToList();

            if (customers != null)
            {
                List<ICustomerDisplayViewModel> customersDisplay = new List<ICustomerDisplayViewModel>();
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

        public ICustomerEditViewModel CreateCustomer()
        {
            var customer = new CustomerEditViewModel()
            {
                CustomerId = Guid.NewGuid().ToString(),
                Countries = _countriesRepo.GetCountries(),
                Regions = _regionsRepo.GetRegions()
            };
            return customer;
        }

        public bool SaveCustomer(ICustomerEditViewModel customeredit)
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
                    customer.Country = _context.Countries.Find(customeredit.SelectedCountryId);
                    customer.Region = _context.Regions.Find(customeredit.SelectedRegionId);

                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return true;
                }
            }
            // Return false if customeredit == null or CustomerID is not a guid
            return false;
        }
    }
}
