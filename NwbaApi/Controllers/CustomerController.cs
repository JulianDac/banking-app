using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using NwbaApi.Models.DataManager;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerManager _repo;
       

        public CustomerController(CustomerManager repo)
        {
            _repo = repo;
        }

        // This api call returns the list of all customers. Address and Account details are not included.
        // call this api to view all the customers from the admin portal after the admin logged.
        // GET: api/customer
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _repo.GetAll();
        }

        // This api call returns particular customer along with address detils by passing customer ID
        // GET api/customer/1
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            var customer = _repo.Get(id);
            var address = _repo.GetAddress(id);
            customer.Address = address;
            return customer;
        }

        // PUT api/customer/1
        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            _repo.Update(customer.CustomerID, customer);
        }


        // This api calls delete customer and the related records from other tables. 
        // customers.customerID, logins.LoginID, accounts.AccountNumber, addresses.AddressID,transactions.TransactionID
        // DELETE api/customers/1
        // [Route("delete/{id}")] // This route is used to check from url
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

        // POST api/customers
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _repo.Add(customer);
        }

    }
}