using NwbaApi.Data;
using NwbaApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Models.DataManager
{
    public class LoginManager : IDataRepository<Login, string>
    {
        private readonly NwbaContext _context;

        public LoginManager(NwbaContext context)
        {
            _context = context;
        }

        public Login Get(string id)
        {
            return _context.Logins.Find(id);
        }
       
        public IEnumerable<Login> GetAll()
        {
            return _context.Logins.ToList();
        }

        // This set the LockFlag to lock by passing loginID
        public bool Lock(string id)
        {
            var login = Get(id);
            if(login == null)
            {
                return false;
            }
            else
            {
                login.LockFlag = LockFlag.Lock;
                _context.SaveChanges();
                return true;
            } 
        }

        // This set the LockFlag to unlock by passing login ID
        public bool UnLock(string id)
        {
            var login = Get(id);
            if (login == null)
            {
                return false;
            }
            else
            {
                login.LockFlag = LockFlag.Unlock;
                _context.SaveChanges();
                return true;
            }
        }

      
        public string Add(Login login)
        {
            _context.Logins.Add(login);
            _context.SaveChanges();

            return login.LoginID;
        }

        public string Update(string id, Login login)
        {
            _context.Update(login);
            _context.SaveChanges();

            return id;
        }

        public string Delete(string id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();

            return id;
        }
    }
}
