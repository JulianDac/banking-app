using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using NwbaApi.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly AccountManager _repo;
        private readonly TransactionManager _transactionManager;

        public AccountController(AccountManager repo, TransactionManager transactionManager)
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

        // Returns transaction details by passing account number and filtered by date's
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
