using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaAdmin.Models
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Tfn { get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }
        public Account Account { get; set; }
   
    }
}
