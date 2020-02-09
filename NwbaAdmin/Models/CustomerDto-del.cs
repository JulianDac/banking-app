///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///   Adapted from Tute lab and modified to suit the requirement
///-----------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaAdmin.Models
{
    public class CustomerDto
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int CustomerID { get; set; }

        //[Required]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string Name { get; set; }

        //[Required, StringLength(20)]
        //public string Tfn { get; set; }

        //public int AddressID { get; set; }
        //public virtual Address Address { get; set; }

        //public virtual List<Account> Accounts { get; set; }
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Tfn { get; set; }
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
       // public CustomerDto Customer { get; set; }
    }
}
