///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.Utilities;
using NwbaSystem.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using NwbaSystem.ViewModels;

namespace NwbaSystem.Controllers
{
    [AuthorizeCustomer]
    public class AccountController : Controller
    {
        private readonly NwbaContext _context;

        private int customerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        private int accountNumber => HttpContext.Session.GetInt32(nameof(Account.AccountNumber)).Value;

        public AccountController(NwbaContext context) => _context = context;

        public async Task<IActionResult> Index(int accountNumber)
        {
            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customers.Include(x => x.Accounts).
               FirstOrDefaultAsync(x => x.CustomerID == customerID);

            var accountNos = customer?.Accounts?.Select(x => x.AccountNumber)?.ToList();
         
            var accountList = _context.Accounts?.Where(x => accountNos.Contains(x.AccountNumber));

            var result = new List<ViewModels.AccountViewModel>();

            foreach (var acc in accountList)
            {
                var accountViewModel = new AccountViewModel();

                accountViewModel.AccountNumber = acc.AccountNumber;
                accountViewModel.AccountType = acc.AccountType.ToString();
                accountViewModel.Balance = acc.Balance;
                accountViewModel.ModifyDate = acc.ModifyDate;
                result.Add(accountViewModel);
            }

            return View(result);
        }

        public async Task<IActionResult> Deposit(int id) => View(await _context.Accounts.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Deposit(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (amount.HasMoreThanTwoDecimalPlaces())
                ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = amount;
                return View(account);
            }

            // Note this code could be moved out of the controller, e.g., into the Model.
            account.Balance += amount;
            account.Transactions.Add(
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow
                });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Withdraw(int id) => View(await _context.Accounts.FindAsync(id));

        [HttpPost]
        public async Task<IActionResult> Withdraw(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Please enter positive amount only.");
            if (amount.HasMoreThanTwoDecimalPlaces())
                ModelState.AddModelError(nameof(amount), "Exactly 2 decimal places only.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = amount;
                return View(account);
            }
            account.Balance -= amount;
            account.Transactions.Add(
                new Transaction
                {
                    TransactionType = TransactionType.Withdraw,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow
                });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //https://www.dotnettricks.com/learn/mvc/return-view-vs-return-redirecttoaction-vs-return-redirect-vs-return-redirecttoroute
        /*
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(AccountController accountID)
        {
            //update account to database
            return RedirectToAction("Index", "Account");
        }
        */
    }
}