///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///   Adapted from Tute lab and modified to suit the requirement
///-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace NwbaSystem.Models
{
    public enum LockFlag
    {
        NotLocked = 0,
        Locked = 1
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
