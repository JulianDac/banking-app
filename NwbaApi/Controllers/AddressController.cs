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
    public class AddressController : Controller
    {
        private readonly AddressRepository _repo;


        public AddressController(AddressRepository repo)
        {
            _repo = repo;
        }

        // Returns all addresses
        // GET: api/address
        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return _repo.GetAll();
        }

        // Returns particular address 
        // GET api/address/1
        [HttpGet("{id}")]
        public Address Get(int id)
        {
            return _repo.Get(id);
        }

        // Returns particular address 
        // GET api/customeraddress/1
        [Route("{id}\address")]
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
