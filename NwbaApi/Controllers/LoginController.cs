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
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginManager _repo;


        public LoginController(LoginManager repo)
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

        // POST api/logins
        [HttpPost]
        public void Post([FromBody] Login login)
        {
            _repo.Add(login);
        }

    }
}
