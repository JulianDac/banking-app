﻿using System;
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
        [ValidateModel]
        public IActionResult Get()
        {
            var customers = _repo.GetAll();

            if (customers.Count() != 0)
            {
                return Ok(customers);
            }
            else
            {
                ModelState.AddModelError("", "No results found");
                return BadRequest();
            }

        }

        // This api call returns particular customer along with address detils by passing customer ID
        // GET: api/customer/1
        [HttpGet]
        [Route("{id}")]
        //[ValidateModel]
        public IActionResult Get(int id)
        {
            var customer = _repo.Get(id);
            if (customer != null)
            {
                var address = _repo.GetAddress(id);
                customer.Address = address;
                return Ok(customer);
            }
            else
            {
                ModelState.AddModelError("", "No customers found");
                return BadRequest();
            }

        }


        // PUT api/customer/1
        [Route("update/{id}")] // This route is used to check from url
        [HttpPut]
        [ValidateModel]
        //public void Put([FromBody] Customer customer)
        //{
        //    _repo.Update(customer.CustomerID, customer);
        //}
        public IActionResult Put([FromBody] Customer customer)
        {
            if (customer == null)
            {
                ModelState.AddModelError("", "Customer not found");
                return BadRequest();
            }
            else
            {
                var result = _repo.Update(customer.CustomerID, customer);
                if (result == customer.CustomerID)
                {
                    return Ok(result);
                }
                else
                {
                    ModelState.AddModelError("", "Customer not found");
                    return BadRequest();
                }
            }
        }

        // This api calls delete customer and the related records from other tables. 
        // customers.customerID, logins.LoginID, accounts.AccountNumber, addresses.AddressID,transactions.TransactionID
        // DELETE api/customers/1
        [Route("delete/{id}")] // This route is used to check from url
        [HttpDelete("{id}")]
        [ValidateModel]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "Invalid customer ID");
                return BadRequest();
            }
            else
            {
                var result = _repo.Delete(id);
                if(result == id)
                {
                    return Ok(result);
                }
                else
                {
                    ModelState.AddModelError("", "Customer not found");
                    return BadRequest();
                }
            }
        }
        // POST api/customers
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _repo.Add(customer);
        }

    }
}