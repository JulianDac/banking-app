///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Rendering;
using NwbaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaSystem.ViewModels
{
    public class BillPayViewModel
    {
       
        public int BillPayID { get; set; }
        public int PayeeID { get; set; }
        public string PayeeName { get; set; }
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Period { get; set; }
        public DateTime? ModifyDate { get; set; }
        public BillPayStatus BillPayStatus { get; set; }

        public string SelectedAccount { get; set; }

        public virtual List<SelectListItem> Accounts { get; set; }

        public string SelectedPayee { get; set; }

        public virtual List<SelectListItem> PayeeList { get; set; }

        public string SelectedPaymentFrequency { get; set; }

        public virtual List<SelectListItem> PaymentFrequency { get; set; }


    }
}
