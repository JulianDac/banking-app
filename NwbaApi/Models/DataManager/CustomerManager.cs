using Microsoft.EntityFrameworkCore;
using NwbaApi.Data;
using NwbaApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Models.DataManager
{
    public class CustomerManager : IDataRepository<Customer, int>
    {
        private readonly NwbaContext _context;

        public CustomerManager(NwbaContext context)
        {
            _context = context;
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public Address GetAddress(int id)
        {
            var customer = _context.Customers.Include(x => x.Address).FirstOrDefault(x => x.CustomerID == id);
            return customer.Address;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public int Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.CustomerID;
        }

        public int Update(int id, Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();

            return id;
        }

        public int Delete(int id) 
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }

        
    }
}
