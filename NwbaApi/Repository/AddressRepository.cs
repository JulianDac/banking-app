///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using NwbaApi.Data;
using System.Collections.Generic;
using System.Linq;

namespace NwbaApi.Models
{
    public class AddressRepository : IDataRepository<Address, int>
    {
        private readonly NwbaContext _context;

        public AddressRepository(NwbaContext context)
        {
            _context = context;
        }

        public Address Get(int id)
        {
            return _context.Addresses.Find(id);
        }

        public Address GetAddress(int id)
        {
            return _context.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public int Add(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();

            return address.AddressID;
        }

        public int Update(int id, Address address)
        {
            _context.Update(address);
            _context.SaveChanges();

            return id;
        }

        public int Delete(int id)
        {
            _context.Addresses.Remove(_context.Addresses.Find(id));
            _context.SaveChanges();

            return id;
        }
    }
}
