///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NwbaApi.Models
{
    public class Payee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayeeID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public int AddressID { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }
}
