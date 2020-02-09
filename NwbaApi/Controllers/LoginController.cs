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
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginRepository _repo;


        public LoginController(LoginRepository repo)
        {
            _repo = repo;
        }

        // GET: api/login
        [HttpGet]
        public IEnumerable<Login> Get()
        {
            return _repo.GetAll();
        }

        // Returns particular login 
        // GET api/login/1
        [HttpGet("{id}")]
        public Login Get(string id)
        {
            return _repo.Get(id);
        }

    }
}
