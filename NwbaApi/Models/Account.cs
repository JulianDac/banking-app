///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///   Adapted from Tute lab and modified to suit the requirement
///-----------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaApi.Models
{
    public enum AccountType
    {
        Checking = 1,
        Saving = 2
    }

    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        [Display(Name = "Type")]
        public AccountType AccountType { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
        public DateTime ModifyDate { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
