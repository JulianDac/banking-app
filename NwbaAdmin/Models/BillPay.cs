///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaAdmin.Models
{
    public enum BillPayStatus
    {
        Blocked = 0,
        ReadyToProcess = 1,
        Success = 2,
        Failed = 3
    }


    public class BillPay
    {
        
        public int BillPayID { get; set; }
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Period { get; set; }
        public DateTime? ModifyDate { get; set; }
        public BillPayStatus BillPayStatus { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}
