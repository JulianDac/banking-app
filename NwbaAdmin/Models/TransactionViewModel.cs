using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaAdmin.Models
{
    public class TransactionViewModel
    { 
        public List<Transaction> Transactions { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int SelectedCustomerNumber { get; set; }
        public List<SelectListItem> CustomerList { get; set; }

     
    }
}
