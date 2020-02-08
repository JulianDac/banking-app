using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.Web.Helper;


namespace NwbaSystem.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly NwbaContext _context;
        private string isAdmin => HttpContext.Session.GetString("isAdmin");

        public AdminHomeController(NwbaContext context) => _context = context;

        // GET: AdminHome
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> UserIndex()
        {
            var response = await NwbaApi.InitializeClient().GetAsync("api/customer");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var customers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(result); // add DTO

            return View(customers); // add View
        }


        public async Task<IActionResult> TransactionIndex(int? accountNumber)
        {

            if (accountNumber == null)
                return NotFound();
            
            var response = await NwbaApi.InitializeClient().GetAsync("api/transactions/{customerID}");

            var chosenAccount = _context.Accounts.FirstOrDefault(acc => acc.AccountNumber.Equals(accountNumber));

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var transactions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Transaction>>(result); // add DTO

            return View(transactions); // add View

        }


        public IActionResult SCheduledPayIndex()
        {

            return View();
        }
    }
}