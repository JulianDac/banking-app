﻿///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaApi.Models
{
    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        ServiceCharge = 4,
        BillPay = 5
    }

    public class Transaction
    {
        public int TransactionID { get; set; }
        [Display(Name="Transaction Type")]
        public TransactionType TransactionType { get; set; }
        [Display(Name="From Account")]
        public int AccountNumber { get; set; }
        [Display(Name="To Account")]
        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountNumber { get; set; }
        [Display(Name="Amount")]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name="Comment")]
        [StringLength(255)]
        public string Comment { get; set; }
        public DateTime TransactionTimeUtc { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
