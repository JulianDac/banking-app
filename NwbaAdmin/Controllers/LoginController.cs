using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NwbaAdmin.Controllers
{
    public class LoginController : Controller
    {
        [Route("/Nwba/yejaman")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string loginID, string password)
        {
            if (loginID == "admin" && password == "admin")
            {
             
                return RedirectToAction("Index", "Customer"); //once logged in, go to Index page of Home 
            }

            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View();
        }


    }
}