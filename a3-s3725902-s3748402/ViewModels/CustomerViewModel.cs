using Microsoft.AspNetCore.Mvc.Rendering;
using NwbaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace NwbaSystem.ViewModels
{
    public class CustomerViewModel
    {

        public int CustomerID { get; set; }
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public int SelectedCustomerAccount { get; set; }
        public virtual List<SelectListItem> AccountsList { get; set; }
        public int? Page { get; set; }
        public IPagedList<Transaction> TransactionsPagedList { get; set; }
        public List<ViewModels.TransactionViewModel> Transactions { get; set; }

    }
}
