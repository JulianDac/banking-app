///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace NwbaApi.Models
{
    public enum LockFlag
    {
        Unlock = 0,
        Lock = 1
    }
    public class Login
    {
        [Required, StringLength(8)]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Required, StringLength(64)]
        public string PasswordHash { get; set; }
        public int FailedAttempts { get; set; }
        public LockFlag LockFlag { get; set; }
        public DateTime? LockTime { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
