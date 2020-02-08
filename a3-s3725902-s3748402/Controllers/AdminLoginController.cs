using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NwbaSystem.Data;
using NwbaSystem.Models;

namespace NwbaSystem.Controllers
{

    [Route("/Nwba/AdminLogin")]
    public class AdminLoginController : Controller
    {
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            if (loginID == "admin" && password == "admin")
            {
                // Login admin.
                HttpContext.Session.SetString("isAdmin", "true");

                return RedirectToAction("Index", "AdminHome"); //once logged in, go to Index page of AdminHome 
            }

            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View();
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "AdminLogin");
        }
    }
}