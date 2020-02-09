///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///   Adapted from Tute lab and modified to suit the requirement
///-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace NwbaAdmin.Models
{
    public enum LockFlag
    {
        Unlock = 0,
        Lock = 1
    }

    public class Login
    {
        public string LoginID { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string PasswordHash { get; set; }
        public int FailedAttempts { get; set; }
        public LockFlag LockFlag { get; set; }
        public DateTime ModifyDate { get; set; }
        public string locklogin { get; set; }
    }
}
