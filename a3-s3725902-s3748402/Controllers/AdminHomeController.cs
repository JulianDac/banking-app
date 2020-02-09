using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult Index()
        {
            return View();
        }

        //GET
        public async Task<IActionResult> UserIndex()
        {
            var response = await NwbaApi.InitializeClient().GetAsync("api/customers");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var customers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(result); 

            return View(customers); 
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

        //TODO [HttpPost]
        [Route("AdminHome/Lock/{customerID}")]
        public async Task<IActionResult> Lock(string customerID)
        {
            if (customerID == null)
                return NotFound();
            var response = await NwbaApi.InitializeClient().PutAsync("api/customers/" + customerID + "/lock", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            return View();
        }

        //TODO [HttpPost]
        [Route("AdminHome/Unlock/{customerID}")]
        public async Task<IActionResult> Unlock(string customerID)
        {
            if (customerID == null)
                return NotFound();
            var response = await NwbaApi.InitializeClient().PutAsync("api/customers/" + customerID + "/unlock", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            return View();
        }

        //TODO [HttpPost]
        [Route("AdminHome/Delete/{customerID}")]
        public async Task<IActionResult> DeleteCustomer(string customerID)
        {
            if (customerID == null)
                return NotFound();
            var response = await NwbaApi.InitializeClient().DeleteAsync("api/customers/" + customerID);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            return View();
        }

        //GET
        [Route("AdminHome/Edit/{customerID}")]
        public async Task<IActionResult> UpdateCustomer(string customerID)
        {
            if (customerID == null)
                return NotFound();

            var response = await NwbaApi.InitializeClient().GetAsync($"api/customers/{customerID}");
            //GetAsync("api/transactions/{customerID}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return View(customer);
        }


        //POST //
        [HttpPost]
        [Route("AdminHome/Edit/{customerID}")]
        public async Task<IActionResult> UpdateCustomer(string customerID, Customer customer)
        {
            if(customerID == null)
                return NotFound();

            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var xxx = await content.ReadAsStringAsync();

            var response = await NwbaApi.InitializeClient().PutAsync("api/customers/" + customerID, content); 

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            return View("SuccessfulUpdate", "AdminHome");
        }

        public async Task<IActionResult> AccountIndex()
        {
            var response = await NwbaApi.InitializeClient().GetAsync("api/accounts");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var accounts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Account>>(result);

            return View(accounts);
        }

        public async Task<IActionResult> TransactionIndex()
        {
            var response = await NwbaApi.InitializeClient().GetAsync("api/transactions");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var transactions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Transaction>>(result);

            return View(transactions);
        }


    }
}