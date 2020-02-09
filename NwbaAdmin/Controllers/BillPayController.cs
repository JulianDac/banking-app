using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NwbaAdmin.Models;
using NwbaAdmin.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NwbaAdmin.Controllers
{
    public class BillPayController : Controller
    {
        private readonly ILogger<BillPayController> _logger;

        public BillPayController(ILogger<BillPayController> logger)
        {
            _logger = logger;
        }

        // GET: Customer/Index
        public async Task<IActionResult> Index()
        {
            var response = await NwbaApi.InitializeClient().GetAsync("api/billpayments");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;
            // Deserializing the response recieved from web api and storing into a list.
            var billpay = JsonConvert.DeserializeObject<List<BillPay>>(result);
            return View(billpay);
        }

        public IActionResult Block(int id)
        {
            if (id < 0)
                return NotFound();

            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            try
            {
                var response = NwbaApi.InitializeClient().PutAsync($"api/billpayments/{id}/block", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["alertMessage"] = "Successfully Blocked";
                    return RedirectToAction("Index");
                }
                else if (!response.IsSuccessStatusCode)
                {
                    TempData["alertMessage"] = "Already Blocked";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                TempData["alertMessage"] = "Block when status is Ready To Process";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Unblock(int id)
        {
            if (id < 0)
                return NotFound();

            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            try
            {
                var response = NwbaApi.InitializeClient().PutAsync($"api/billpayments/{id}/unblock", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["alertMessage"] = "Successfully Unblocked";
                    return RedirectToAction("Index");
                }
                else if(!response.IsSuccessStatusCode)
                {
                    TempData["alertMessage"] = "Already Unblocked";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                TempData["alertMessage"] = "Unblock when status is blocked";
            }
            return RedirectToAction("Index");
        }

    }
}

