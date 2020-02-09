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
    public class LoginRepository : IDataRepository<Login, string>
    {
        private readonly NwbaContext _context;

        public LoginRepository(NwbaContext context)
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
