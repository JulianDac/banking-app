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

namespace NwbaAdmin.Models
{
    public enum AccountType
    {
        Checking = 1,
        Saving = 2
    }

    public class Account
    {
        
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        
        public decimal Balance { get; set; }
        public DateTime ModifyDate { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
