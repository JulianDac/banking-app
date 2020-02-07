
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaAdmin.Models
{
    public class CustomerDto
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

       // [Required]
       // [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

       // [Required, StringLength(20)]
        public string Tfn { get; set; }

        public int AddressID { get; set; }
       // public virtual Address Address { get; set; }

      //  public virtual List<Account> Accounts { get; set; }


    }
}
