﻿///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using NwbaApi.Data;
using System.Collections.Generic;
using System.Linq;

namespace NwbaApi.Models
{
    public class CustomerRepository : IDataRepository<Customer, int>
    {
        private readonly NwbaContext _context;

        public CustomerRepository(NwbaContext context)
        {
            _context = context;
        }

        
        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        // Returns address for the customer ID
        public Address GetAddress(int id)
        {
            var customer = _context.Customers.Include(x => x.Address).FirstOrDefault(x => x.CustomerID == id);
            return customer.Address;
        }

        // Retruns all the accounts for the customer ID
        public List<Account> GetAccounts(int id)
        {
            var customer = _context.Customers.Include(x => x.Accounts).FirstOrDefault(x => x.CustomerID == id);
            var accounts = customer.Accounts;
            return accounts;
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
            var c = _context.Customers.Include(x => x.Address).FirstOrDefault(x => x.CustomerID == id);
            if (c != null)
            {
                c.Name = customer.Name;
                c.Tfn = customer.Tfn;
                if(customer.Address != null)
                {
                    c.Address.Street = customer.Address.Street;
                    c.Address.City = customer.Address.City;
                    c.Address.State = customer.Address.State;
                    c.Address.PostCode = customer.Address.PostCode;
                    c.Address.Phone = customer.Address.Phone;
                }
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

        /* 
         * Paas the customer id to lock the account login
         * returns true if the LockFlag is set to Lock otherwise returns false
         */

        public bool Lock(int id)
        {
            var login = _context.Logins.Include(x => x.Customer).FirstOrDefault(x => x.CustomerID == id);
            if(login != null)
            {
                LoginRepository lg = new LoginRepository(_context);
                lg.Lock(login.LoginID);
                return true;
            }
            else
            {
                return false;
            }
        }

        /* 
         * Paas the customer id to unlock the account login
         * returns true if the LockFlag is set to UnLock otherwise returns false
         */
        public bool UnLock(int id)
        {
            var login = _context.Logins.Include(x => x.Customer).FirstOrDefault(x => x.CustomerID == id);
            if (login != null)
            {
                LoginRepository lg = new LoginRepository(_context);
                lg.UnLock(login.LoginID);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
