
///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------


using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.Attributes;
using NwbaSystem.ViewModels;
using System.Linq;
using System.Net.Mime;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using NwbaSystem.Utilities;
using ClassLibraryTransaction;

namespace NwbaSystem.Controllers
{
    [AuthorizeCustomer]
    public class CustomerController : Controller
    {
        private readonly NwbaContext _context;

        // ReSharper disable once PossibleInvalidOperationException
        private int customerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public CustomerController(NwbaContext context) => _context = context;

        public async Task<IActionResult> Index()        {
            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customers.FindAsync(customerID);

            var transactionViewModel = new TransactionViewModel();
            transactionViewModel.Customer = customer;

            return View(transactionViewModel);
        }

        public async Task<IActionResult> Transaction(int? page = 1)
        {
            int accId = 4100;
            // Page the orders, maximum of 3 per page.
            const int pageSize = 3;
            var pagedList =  await _context.Transactions.Where(x => x.AccountNumber == accId).
                                ToPagedListAsync(page, pageSize);

            return View(pagedList);
        }
        

        //asp-action="Result" from View is passed back here
        [HttpPost]                     
        public async Task<IActionResult> Result(TransactionViewModel transactionViewModel) {
            var account = await _context.Accounts.FindAsync(transactionViewModel.FromAccount); //can only find primary key attribute
            var beneficiary = await _context.Accounts.FindAsync(transactionViewModel.DestinationAccountNumber);

            var customer = await _context.Customers.FindAsync(customerID);
            transactionViewModel.Customer = customer;
            
            if (transactionViewModel.Amount <= 0)
            {
                ModelState.AddModelError(nameof(transactionViewModel.Amount), "Amount must be positive.");
                return View("Index", transactionViewModel);
            }
            if (transactionViewModel.Amount.HasMoreThanTwoDecimalPlaces()) {
                ModelState.AddModelError(nameof(transactionViewModel.Amount), "Amount cannot have more than 2 decimal places.");
                return View("Index", transactionViewModel);
            }
            if ((transactionViewModel.TransactionType != "D") & (transactionViewModel.Amount > account.Balance))
            {
                ModelState.AddModelError(nameof(transactionViewModel.Amount), "Amount exceeds current balance.");
                return View("Index", transactionViewModel);
            }
            if (transactionViewModel.DestinationAccountNumber.ToString() == transactionViewModel.FromAccount.ToString())
            {
                ModelState.AddModelError(nameof(transactionViewModel.DestinationAccountNumber), "Can't transfer to same account");
                return View("Index", transactionViewModel);
            }
            if ((transactionViewModel.TransactionType == "T") & (beneficiary == null)) 
            { 
                ModelState.AddModelError(nameof(transactionViewModel.DestinationAccountNumber), "Can't find this account");
                return View("Index", transactionViewModel);
            }
            
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = transactionViewModel.Amount;
                return View(transactionViewModel); 
            }

            //separate business logic
            var transactionBusiness = new TransactionBusinessLogic();
            transactionBusiness.ProcessTransaction(account, beneficiary, transactionViewModel);

            switch (transactionViewModel.TransactionType)
            {
                case ("D"):
                    account.Balance += transactionViewModel.Amount;
                    account.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Deposit,
                            Amount = transactionViewModel.Amount,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    break;
                case ("W"):
                    account.Balance -= transactionViewModel.Amount;
                    account.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Withdraw,
                            Amount = transactionViewModel.Amount,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    break;
                case ("T"):
                    account.Balance -= transactionViewModel.Amount;
                    beneficiary.Balance += transactionViewModel.Amount;
                    account.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Transfer,
                            Amount = transactionViewModel.Amount,
                            DestinationAccountNumber =  transactionViewModel.DestinationAccountNumber,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    beneficiary.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Transfer,
                            Amount = transactionViewModel.Amount,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    break;
            }
            
            await _context.SaveChangesAsync();
            
            return View(transactionViewModel);
            
        }
    }
}
