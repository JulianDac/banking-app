///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaSystem.Models
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
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillPayID { get; set; }
        [StringLength(30)]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string Period { get; set; }

        public DateTime? ModifyDate { get; set; }

        [Display(Name = "BillPay Status")]
        public BillPayStatus BillPayStatus { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }
}
