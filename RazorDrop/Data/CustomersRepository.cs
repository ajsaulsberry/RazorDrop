using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RazorDrop.Models;
using RazorDrop.ViewModels;

namespace RazorDrop.Data
{
    public class CustomersRepository
    {
        private readonly RazorDropContext _context;
        public CustomersRepository(RazorDropContext context)
        {
            _context = context;
        }

        public List<CustomerDisplayViewModel> GetCustomers()
        {

            List<Customer> customers = new List<Customer>();
            customers = _context.Customers.AsNoTracking()
                .Include(x => x.Country)
                .Include(x => x.Region)
                .ToList();

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


        public CustomerEditViewModel CreateCustomer()
        {
            var cRepo = new CountriesRepository(_context);
            var rRepo = new RegionsRepository(_context);
            var customer = new CustomerEditViewModel()
            {
                CustomerId = Guid.NewGuid().ToString(),
                Countries = cRepo.GetCountries(),
                Regions = rRepo.GetRegions()
            };
            return customer;
        }

        public bool SaveCustomer(CustomerEditViewModel customeredit)
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
