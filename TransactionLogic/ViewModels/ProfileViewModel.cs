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
    public class ProfileViewModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Tfn { get; set; }
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        public string Phone { get; set; }  
        public  Address Address { get; set; }
        public Customer Customer { get; set; }

    }
}
