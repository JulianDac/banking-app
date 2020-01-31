///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.ViewModels;
using X.PagedList;

namespace NwbaSystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly NwbaContext _context;
        private int customerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        private string selectedAccountSession = "selectedAccountNumber";
        public TransactionController(NwbaContext context) => _context = context;

       
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.Include(x => x.Accounts).
               FirstOrDefaultAsync(x => x.CustomerID == customerID);

            var accountNumbers = customer?.Accounts?.Select(x => x.AccountNumber)?.ToList();
            var transactions = _context.Transactions?.Where(x => accountNumbers.Contains(x.AccountNumber));
            var result = new List<ViewModels.TransactionViewModel>();

            foreach (var transaction in transactions)
            {
                var transactionModel = new TransactionViewModel();

                transactionModel.TransactionID = transaction.TransactionID;
                transactionModel.TransactionType = transaction.TransactionType.ToString();
                transactionModel.FromAccount = transaction.AccountNumber;
                transactionModel.DestinationAccountNumber =Convert.ToInt32(transaction.DestinationAccountNumber);
                transactionModel.Amount = transaction.Amount;
                transactionModel.Comment = transaction.Comment;
                transactionModel.TransactionTimeUtc = transaction.TransactionTimeUtc;
                transactionModel.ModifyDate = transaction.ModifyDate;

                result.Add(transactionModel);
            }
            return View(result);
        }

        // Transaction history page: getting customer and his/her all the account details
        public async Task<IActionResult> AccountSelection(int page=1)
        {
            var customer = await _context.Customers.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.CustomerID == customerID);

            List<SelectListItem> accountsList = new List<SelectListItem>();
            foreach (var account in customer?.Accounts)
            {
                accountsList.Add(new SelectListItem()
                {
                    Text = account.AccountNumber.ToString() + " - " + account.AccountType.ToString(),
                    Value = account.AccountNumber.ToString()

                });
            }
            var cvm = new CustomerViewModel();
            cvm.CustomerID = customer.CustomerID;
            int selectedAccountNumber = 0;
            HttpContext.Session.GetString(selectedAccountSession);

            if( page == 1 || string.IsNullOrEmpty(HttpContext.Session.GetString(selectedAccountSession)))
            {
                selectedAccountNumber = customer.Accounts.FirstOrDefault(x => x.CustomerID == customerID).AccountNumber;
                HttpContext.Session.SetString(selectedAccountSession, selectedAccountNumber.ToString());
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(selectedAccountSession)))
            {
                selectedAccountNumber =  Convert.ToInt32(HttpContext.Session.GetString(selectedAccountSession)) ;
            }
            
            cvm.AccountNumber = selectedAccountNumber;
            cvm.AccountBalance = customer.Accounts.FirstOrDefault(x => x.CustomerID == customerID).Balance;
            cvm.AccountType = customer.Accounts.FirstOrDefault(x => x.CustomerID == customerID).AccountType.ToString();
            cvm.AccountsList = accountsList;
            cvm.SelectedCustomerAccount = selectedAccountNumber;
            HttpContext.Session.SetString(selectedAccountSession, selectedAccountNumber.ToString());

            var transactions =  _context.Transactions.Where(x => x.AccountNumber == selectedAccountNumber);
            var TotalClients = transactions?.Count() ?? 0;
            var transactionViewModel = BuildTransactionModel(selectedAccountNumber);
            const int pageSize = 4;
            var a = await _context.Accounts.FindAsync(selectedAccountNumber);
            var pagedList = await a.Transactions.ToPagedListAsync(page, pageSize);
            cvm.TransactionsPagedList = pagedList;
            return View(cvm);
        }


        [HttpPost]
        public async Task<IActionResult> AccountSelection(CustomerViewModel viewModel )
        {
            
            var customer = await _context.Customers.Include(x => x.Accounts).FirstOrDefaultAsync(x => x.CustomerID == customerID);

            List<SelectListItem> accountsList = new List<SelectListItem>();

            foreach (var account in customer?.Accounts)
            {
                accountsList.Add(new SelectListItem()
                {
                    Text = account.AccountNumber.ToString() + " - " + account.AccountType.ToString(),
                    Value = account.AccountNumber.ToString()

                });
            }
            var cvm = new CustomerViewModel();

            cvm.CustomerID = customer.CustomerID;
            var selectedAccountNumber = viewModel.SelectedCustomerAccount;
            HttpContext.Session.SetString(selectedAccountSession, selectedAccountNumber.ToString());
            var selectedAccount = customer.Accounts.FirstOrDefault(x => x.AccountNumber == selectedAccountNumber && x.CustomerID == customer.CustomerID);
            var acctNo = selectedAccount.AccountNumber;
            cvm.AccountBalance = selectedAccount.Balance;
            cvm.AccountType = selectedAccount.AccountType.ToString();
            cvm.AccountsList = accountsList;
            cvm.SelectedCustomerAccount = acctNo;
            cvm.AccountNumber = acctNo;

            var transactions = _context.Transactions.Where(x => x.AccountNumber == selectedAccountNumber);
            var transactionViewModel =  BuildTransactionModel(selectedAccountNumber);

            const int page = 1;
            const int pageSize = 4;
            var a = await _context.Accounts.FindAsync(selectedAccountNumber);
            var pagedList = await a.Transactions.ToPagedListAsync(page, pageSize);
            cvm.TransactionsPagedList = pagedList;
            return View(cvm);
        }

        private List<TransactionViewModel> BuildTransactionModel(int selectedAccountNumber)
        {
            var transactions = _context.Transactions.Where(x => x.AccountNumber == selectedAccountNumber);
            List<TransactionViewModel>  transactionViewModel = new List<ViewModels.TransactionViewModel>();
            foreach (var transaction in transactions)
            {
                var transactionModel = new TransactionViewModel();
                transactionModel.TransactionID = transaction.TransactionID;
                transactionModel.TransactionType = transaction.TransactionType.ToString();
                transactionModel.FromAccount = transaction.AccountNumber;
                transactionModel.DestinationAccountNumber = Convert.ToInt32(transaction.DestinationAccountNumber);
                transactionModel.Amount = transaction.Amount;
                transactionModel.Comment = transaction.Comment;
                transactionModel.TransactionTimeUtc = transaction.TransactionTimeUtc;
                transactionModel.ModifyDate = transaction.ModifyDate;

                transactionViewModel.Add(transactionModel);
            }
            return transactionViewModel;
        }
    }
}