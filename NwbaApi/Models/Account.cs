///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
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
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
