///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using System.Collections.Generic;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : Controller
    {
        private readonly TransactionRepository _repo;

        public TransactionController(TransactionRepository repo)
        {
            _repo = repo;
        }

        // Returns all transactions from the database
        // GET: api/transaction
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _repo.GetAll();
        }

        


        // Returns specific transaction by passing transaction ID
        // GET api/transaction/1
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
