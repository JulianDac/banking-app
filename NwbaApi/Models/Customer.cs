///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaApi.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }
        [Required, StringLength(20)]
        public string Tfn { get; set; }
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}
