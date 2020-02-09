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
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressID { get; set; }
        [Required, StringLength(50)]
        public string Street { get; set; }
        [StringLength(20)]
        public string City { get; set; }
        [StringLength(20)]
        public string State { get; set; }
        public string PostCode { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
    }
}
