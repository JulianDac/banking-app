using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using NwbaApi.Models.DataManager;
using System.Collections.Generic;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly AddressManager _repo;


        public AddressController(AddressManager repo)
        {
            _repo = repo;
        }

        // GET: api/address
        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return _repo.GetAll();
        }

        // Returns particular customer 
        // GET api/address/1
        [HttpGet("{id}")]
        public Address Get(int id)
        {
            return _repo.Get(id);
        }

        // Returns particular customer 
        // GET api/customeraddress/1
        [Route("address/{id}")]
        [HttpGet]
        public Address GetCustomerAddress(int id)
        {
            var result = _repo.GetAddress(id);
            return result;
        }

        // POST api/address
        [HttpPost]
        public void Post([FromBody] Address address)
        {
            _repo.Add(address);
        }

        // PUT api/address
        [HttpPut]
        public void Put([FromBody] Address address)
        {
            _repo.Update(address.AddressID, address);
        }

        // DELETE api/address/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
