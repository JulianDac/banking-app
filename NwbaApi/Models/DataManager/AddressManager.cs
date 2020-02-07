using NwbaApi.Data;
using NwbaApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Models.DataManager
{
    public class AddressManager : IDataRepository<Address, int>
    {
        private readonly NwbaContext _context;

        public AddressManager(NwbaContext context)
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
