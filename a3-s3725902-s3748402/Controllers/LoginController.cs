///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///   Adapted from Tute lab and modified to suit the requirement
///-----------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.ViewModels;
using SimpleHashing;

namespace NwbaSystem.Controllers
{
    [Route("/Nwba/SecureLogin")]
    public class LoginController : Controller
    {
        private readonly NwbaContext _context;
        private int customerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public LoginController(NwbaContext context) => _context = context;

        public IActionResult Login() => View();

        [Route("PasswordChange")]
        public async Task<IActionResult> PasswordChange()
        {

            return View();
        }

        [Route("PasswordChange")]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(string PasswordHash, string cpassword)
        {

            if (PasswordHash.Equals(cpassword))
            {
                var newPassword = PBKDF2.Hash(PasswordHash, 50000, 32);

                var login = await _context.Logins.Include(x => x.Customer).FirstOrDefaultAsync(x => x.CustomerID == customerID);

                login.PasswordHash = newPassword;
                login.ModifyDate = DateTime.Now;

                await _context.SaveChangesAsync();
                ModelState.AddModelError("PasswordMessage", "Password succesufully updated.");
            }
            else
            {
                ModelState.AddModelError("PasswordMessage", "Password does not match.");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            var login = await _context.Logins.FindAsync(loginID);
            if (login == null || !PBKDF2.Verify(login.PasswordHash, password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { LoginID = loginID });
            }

            // Login customer.
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            //return RedirectToAction("Index", "Account"); 
            return RedirectToAction("Index", "Customer"); //once logged in, redirect to Controller Customer, action method Index
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
