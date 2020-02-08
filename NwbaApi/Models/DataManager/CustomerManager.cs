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

        // rreturns address for the customer ID
        public Address GetAddress(int id)
        {

            var customer = _context.Customers.Include(x => x.Address).FirstOrDefault(x => x.CustomerID == id);
            customer.Address.Customers = null; // To eliminate possible object cycle error
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
        // This method find the customer, if exists updates the customer
        // else return -1 for failure 
        public int Update(int id, Customer customer)
        {
            var c = Get(id);
            if(c != null)
            {
                _context.Update(customer);
                _context.SaveChanges();
                return id;
            }
            else
            {
                return -1;
            }
            
        }

        // This method find the customer, if exists delete the customer and returns customer id 
        // else return -1 for failure 
        public int Delete(int id) 
        {
            var customer = Get(id);
            if(customer != null)
            {
                _context.Customers.Remove(_context.Customers.Find(id));
                _context.SaveChanges();
                return id;
            }
            else
            {
                return -1;
            }
        }

        
    }
}
