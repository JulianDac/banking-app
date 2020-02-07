using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaAdmin.Models
{
    public class AddressDto
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressID { get; set; }

       // [Required, StringLength(50)]
        public string Street { get; set; }

       // [StringLength(20)]
        public string City { get; set; }

       // [StringLength(20)]
        public string State { get; set; }

       // [StringLength(4)]
        public int PostCode { get; set; }

      //  [StringLength(15)]
        public string Phone { get; set; }

    }
}
