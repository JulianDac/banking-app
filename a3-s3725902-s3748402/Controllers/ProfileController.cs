///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.ViewModels;
using System;
using System.Threading.Tasks;
using NwbaSystem.Utilities;
using System.Text.RegularExpressions;

namespace NwbaSystem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly NwbaContext _context;
        private int customerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public ProfileController(NwbaContext context) => _context = context;
        public async Task<IActionResult> Index()
        {
            // Lazy loading.
            var customer = await _context.Customers.FindAsync(customerID);
            return View(customer);
        }
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.FindAsync(customerID).Result;

            return View(
                new ProfileViewModel
                {
                    CustomerID = customerID,
                    Name = customer.Name,
                    Tfn = customer.Tfn,
                    AddressID = customer.AddressID,
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    Phone = customer.Address.Phone,
                    PostCode = customer.Address.PostCode
                });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel viewModel)
        {
            viewModel.Customer = await _context.Customers.FindAsync(customerID);

            if (viewModel.Name == null)
            {
                ModelState.AddModelError(nameof(viewModel.Name), "Name should not be empty");
                return View(viewModel);
            }
            if (!viewModel.Name.IsAllLetters())
            {
                ModelState.AddModelError(nameof(viewModel.Name), "Only letter are allowed");
                return View(viewModel);
            }
            if ((!viewModel.Tfn.IsAllDigits()) || (viewModel.Tfn.Length <= 7) || (viewModel.Tfn.Length >= 10))
            {
                ModelState.AddModelError(nameof(viewModel.Tfn), "Invalid TFN");
                return View(viewModel);
            }
            if (!viewModel.Phone.IsValidPhone())
            {
                ModelState.AddModelError(nameof(viewModel.Phone), "Invalid Phone");
                return View(viewModel);
            }

            if (!viewModel.City.IsAllLetters())
            {
                ModelState.AddModelError(nameof(viewModel.City), "Only letter are allowed");
                return View(viewModel);
            }
            string zip = viewModel.PostCode.ToString();
            if ((!zip.IsAllDigits()) || (zip.Length <= 3) || (zip.Length >= 5))
            {
                ModelState.AddModelError(nameof(viewModel.PostCode), "Invalid PostCode");
                return View(viewModel);
            }


            viewModel.Customer.Name = viewModel.Name.Trim();
            viewModel.Customer.Tfn = viewModel.Tfn.Trim();
            viewModel.Customer.Address.Street = viewModel.Street.Trim();
            viewModel.Customer.Address.City = viewModel.City.Trim();
            viewModel.Customer.Address.State = viewModel.State;
            viewModel.Customer.Address.PostCode = viewModel.PostCode;
            viewModel.Customer.Address.Phone = viewModel.Phone;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}