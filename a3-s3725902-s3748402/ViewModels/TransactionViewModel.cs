///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NwbaSystem.Models;

namespace NwbaSystem.ViewModels
{
    public class TransactionViewModel
    {
        public Customer Customer { get; set; }
        public int TransactionID { get; set; }
        public string TransactionType { get; set; }
        public int FromAccount { get; set; }
        public Account Account { get; set; }
      
        public int DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime TransactionTimeUtc { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}

