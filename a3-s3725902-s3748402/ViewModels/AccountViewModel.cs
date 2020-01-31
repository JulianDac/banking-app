///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------


using NwbaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaSystem.ViewModels
{
    

    public class AccountViewModel
    {

        public int AccountNumber { get; set; }

        public string AccountType { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public decimal Balance { get; set; }

        public DateTime ModifyDate { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
