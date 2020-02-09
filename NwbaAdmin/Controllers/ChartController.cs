using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NwbaAdmin.Models;
using NwbaAdmin.Web.Helper;
using static System.Net.Mime.MediaTypeNames;

namespace NwbaAdmin.Controllers
{
    public class ChartController : Controller
    {




        // GET: Transaction/Index
        public async Task<IActionResult> Index()
        {
            var tranResult = await NwbaApi.InitializeClient().GetAsync("api/transactions");
            var resultTransactions = tranResult.Content.ReadAsStringAsync().Result;
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(resultTransactions);
            //
            var custResult = await NwbaApi.InitializeClient().GetAsync("api/customers");
            var resultCustomer = custResult.Content.ReadAsStringAsync().Result;
            var customers = JsonConvert.DeserializeObject<List<Customer>>(resultCustomer);

            TransactionViewModel tvm = new TransactionViewModel();

            tvm.Transactions = transactions;
            tvm.FromDate = DateTime.MinValue;
            tvm.ToDate = DateTime.MaxValue;



            List<SelectListItem> customerList = new List<SelectListItem>();
            customerList.Add(new SelectListItem()
            {
                Text = "All Transactions",
                Value = "All Transactions"
            });

            foreach (var customer in customers)
            {

                customerList.Add(new SelectListItem()
                {
                    Text = customer.CustomerID.ToString() + " - " + customer.Name,
                    Value = customer.CustomerID.ToString()
                });
            }

            tvm.CustomerList = customerList;

           






            return View(tvm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TransactionViewModel viewModel)
        {
            var custResult = await NwbaApi.InitializeClient().GetAsync("api/customers");
            var resultCustomer = custResult.Content.ReadAsStringAsync().Result;
            var customers = JsonConvert.DeserializeObject<List<Customer>>(resultCustomer);

            List<int> customerNumbers = new List<int>();

            List<SelectListItem> customerList = new List<SelectListItem>();

            customerList.Add(new SelectListItem()
            {
                Text = "All Transactions",
                Value = "All Transactions"
            });

            foreach (var customer in customers)
            {

                customerList.Add(new SelectListItem()
                {
                    Text = customer.CustomerID.ToString() + " - " + customer.Name,
                    Value = customer.CustomerID.ToString()
                });
            }

            var selectedCustomerNumber = viewModel.SelectedCustomerNumber;
            var fromDate = viewModel.FromDate;
            var toDate = viewModel.ToDate;

            var fDate = String.Format("{0:yyyy-MM-dd}", fromDate);
            var tDate = String.Format("{0:yyyy-MM-dd}", toDate);

            string query = "";

            if (selectedCustomerNumber == 0)
            {
                query = "api/transactions";
            }
            else
            {
                query = "api/customers/" + selectedCustomerNumber + "/transactions?from=" + fDate + "&to=" + tDate + "";

            }
            var tranResult = await NwbaApi.InitializeClient().GetAsync(query);
            var resultTransactions = tranResult.Content.ReadAsStringAsync().Result;
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(resultTransactions);

            TransactionViewModel tvm = new TransactionViewModel();
            tvm.Transactions = transactions;
            tvm.FromDate = viewModel.FromDate;
            tvm.ToDate = viewModel.ToDate;
            tvm.SelectedCustomerNumber = selectedCustomerNumber;
            tvm.CustomerList = customerList;
            return View(tvm);
        }



    }
}
