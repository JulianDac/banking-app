///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using System;
using System.Collections.Generic;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly AccountRepository _repo;
        private readonly TransactionRepository _transactionManager;

        public AccountController(AccountRepository repo, TransactionRepository transactionManager)
        {
            _repo = repo;
            _transactionManager = transactionManager;
        }

        // Returns all transactions from the database
        // GET: api/transaction
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _repo.GetAll();
        }

        // Returns specific transaction by passing transaction ID
        // GET api/transaction/1
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return _repo.Get(id);
        }

        // Returns transaction details by passing account number and filtered by date
        // GET api/transaction/1
        [Route("{id}/transactions")]
        [HttpGet]
        public IActionResult GetAccountTransaction(int id, DateTime? from, DateTime? to)
        {
            List<int> accNumbers = new List<int>();
            accNumbers.Add(id);
            var result = _transactionManager.GetAccountTransactions(accNumbers, from, to);
            return Ok(result);
        }

    }
}
