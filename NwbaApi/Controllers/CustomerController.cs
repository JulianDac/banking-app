﻿///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using System;
using System.Linq;
using System.Collections.Generic;
//using NwbaAdmin.Models;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _repo;

        private readonly TransactionRepository _transactionManager;

        public CustomerController(CustomerRepository repo, TransactionRepository transactionManager)
        {
            _repo = repo;
            _transactionManager = transactionManager;
        }

        #region customer 

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
                var accounts = _repo.GetAccounts(id);
                customer.Accounts = accounts;
                return Ok(customer);
            }
            else
            {
                ModelState.AddModelError("", "No customers found");
                return BadRequest();
            }

        }

        // PUT api/customer/1
        [Route("{id}")] // This route is used to check from url
        [HttpPut]
        [ValidateModel]
        public IActionResult Put(int id, Customer customer)
        {
            if (customer == null)
            {
                ModelState.AddModelError("", "Customer not found");
                return BadRequest();
            }
            else
            {
                if ( _repo.Get(id) == null)
                {
                    ModelState.AddModelError("", "Customer not found");
                    return NotFound();
                }

                var result = _repo.Update(id,customer);

                return Ok();
            }
        }

        // This api calls delete customer and the related records from other tables. 
        // customers.customerID, logins.LoginID, accounts.AccountNumber, addresses.AddressID,transactions.TransactionID
        // DELETE api/customers/1
        [Route("{id}")] // This route is used to check from url
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
        #endregion

        #region identity
        // This api is used to lock the customer account by passing customer ID
        // LOCK api/lock/1
        [Route("{id}/lock")] 
        [HttpPut]
        [ValidateModel]
        public IActionResult lockIdentity(int id) 
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "Invalid customer ID");
                return BadRequest();
            }
            else
            {
                var result = _repo.Lock(id);
                if (result == true)
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
        

         // This api is used to unlock the customer account by passing customer ID
         // UNLOCK api/unlock/1
        [Route("{id}/unlock")]
        [HttpPut]
        [ValidateModel]
        public IActionResult UnLock(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "Invalid customer ID");
                return BadRequest();
            }
            else
            {
                var result = _repo.UnLock(id);
                if (result == true)
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
        #endregion

        #region Transactions

        // Get transaction details for the customer ID  with date filters
        [HttpGet]
        [Route("{id}/transactions")]
        [ValidateModel]
        public IActionResult GetTransactions(int id, DateTime? from, DateTime? to )
        {
            var customer = _repo.Get(id);
            if (customer != null)
            {
                var accounts = _repo.GetAccounts(id);
                if(accounts != null)
                {
                    var accountnumbers = accounts.Select(x => x.AccountNumber).ToList();

                  var transactions =  _transactionManager.GetAccountTransactions(accountnumbers,from,to);
                    return Ok(transactions);
                }
                return NotFound();
            }
            else
            {
                ModelState.AddModelError("", "No customers found");
                return NotFound();
            }
        }

        [Route("{id}/transactions/graph1")]
        [HttpGet]
        [ValidateModel]
        public IActionResult GetTransactionsGraphDataOne(int id, DateTime? from, DateTime? to)
        {
            var customer = _repo.Get(id);
            if (customer != null)
            {
                var accounts = _repo.GetAccounts(id);
                if (accounts != null)
                {
                    var accountnumbers = accounts.Select(x => x.AccountNumber).ToList();

                    var transactions = _transactionManager.GetAccountTransactions(accountnumbers, from, to);

                    var result = new List<GraphDataOne>();
                    foreach(var a in transactions)
                    {
                        result.Add(new GraphDataOne { TransactionDate = a.TransactionTimeUtc, Amount = a.Amount });
                    }
                    return Ok(result);
                }
                return NotFound();
            }
            else
            {
                ModelState.AddModelError("", "No customers found");
                return NotFound();
            }
        }
        #endregion
    }
}