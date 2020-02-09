using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NwbaAdmin.Models;
using NwbaAdmin.Web.Helper;

namespace NwbaAdmin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CustomerController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Customer/Index
        public async Task<IActionResult> Index()
        {
            var response = await NwbaApi.InitializeClient().GetAsync("api/customers");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var customer = JsonConvert.DeserializeObject<List<Customer>>(result);
            
            
            return View(customer);
        }

        // GET: Customer/Edit/1
        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaApi.InitializeClient().GetAsync($"api/customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            Customer c = new Customer();
            c.CustomerID = customer.CustomerID;
            c.Name = customer.Name;
            c.Tfn = customer.Tfn;
            c.Street = customer.Address.Street;
            c.City = customer.Address.City;
            c.State = customer.Address.State;
            c.PostCode = customer.Address.PostCode;
            c.Phone = customer.Address.Phone;

            return View(c);
        }
        // GET: Customer/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaApi.InitializeClient().GetAsync($"api/customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            Customer c = new Customer();
            c.CustomerID = customer.CustomerID;
            c.Name = customer.Name;
            c.Tfn = customer.Tfn;
            c.AddressID = customer.Address.AddressID;
            c.Street = customer.Address.Street;
            c.City = customer.Address.City;
            c.State = customer.Address.State;
            c.PostCode = customer.Address.PostCode;
            c.Phone = customer.Address.Phone;

            return View(c);
        }

        // POST: Customer/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerDto customer)
        {
            var id = customer.CustomerID;
            if (id != customer.CustomerID)
                return NotFound();

            if (ModelState.IsValid)
            {
                Customer cust = new Customer();
                cust.CustomerID = customer.CustomerID;
                cust.Tfn = customer.Tfn;
                cust.Name = customer.Name;
                cust.AddressID = customer.AddressID;

                Address adder = new Address();
                adder.AddressID = customer.AddressID;
                adder.Street = customer.Street;
                adder.City = customer.City;
                adder.State = customer.State;
                adder.PostCode = customer.PostCode;
                adder.Phone = customer.Phone;

                cust.Address = adder;
                cust.Account = null;

                var content = new StringContent(JsonConvert.SerializeObject(cust), Encoding.UTF8, "application/json");

                try
                {
                    var response = NwbaApi.InitializeClient().PutAsync($"api/customers/{id}", content).Result;

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");

                }
                catch (Exception e)
                {
                    var error = e.Message;
                    //todo:
                }
            }

            return View(customer);
        }

        // GET: Customer/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaApi.InitializeClient().GetAsync($"api/customers/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return View(customer);
        }

        // POST: Customer/Delete/1
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var response = NwbaApi.InitializeClient().DeleteAsync($"api/customers/{id}").Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return NotFound();
        }

       // [HttpPost]
        public async Task<IActionResult> Lock(int id)
        {
            if (id < 0)
                return NotFound();
            Login locklg = new Login();
            locklg.locklogin = "true";

            var content = new StringContent(JsonConvert.SerializeObject(locklg), Encoding.UTF8, "application/json");
            try
            {
                var response = NwbaApi.InitializeClient().PutAsync($"api/customers/{id}/lock", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return RedirectToAction("Index");
        }
        // [HttpPost]
        public async Task<IActionResult> Unlock(int id)
        {
            if (id < 0)
                return NotFound();
            Login locklg = new Login();
            locklg.locklogin = "true";

            var content = new StringContent(JsonConvert.SerializeObject(locklg), Encoding.UTF8, "application/json");
            try
            {
                var response = NwbaApi.InitializeClient().PutAsync($"api/customers/{id}/unlock", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                var error = e.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
