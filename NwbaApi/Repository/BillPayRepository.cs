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
    public class BillPayRepository : IDataRepository<BillPay, int>
    {
        private readonly NwbaContext _context;

        public BillPayRepository(NwbaContext context)
        {
            _context = context;
        }

        public BillPay Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        public IEnumerable<BillPay> GetAll()
        {
            return _context.BillPays.ToList();
        }

        public int Add(BillPay billpay)
        {
            _context.BillPays.Add(billpay);
            _context.SaveChanges();

            return billpay.BillPayID;
        }

        public int Update(int id, BillPay billpay)
        {
            _context.Update(billpay);
            _context.SaveChanges();

            return id;
        }

        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();

            return id;
        }

        // Set the bill pay status to block. Only when status is ReadyToProcess. 
        // Pass the billpay ID
        public bool Block(int id)
        {
            var billpay = Get(id);
            if (billpay == null)
            {
                return false;
            }
            else
            {
                billpay.BillPayStatus = BillPayStatus.Blocked;
                _context.SaveChanges();
                return true;
            }
        }

        // Set the bill pay status to unblock. only when status is Blocked.
        // Pass the billpay ID
        public bool UnBlock(int id)
        {
            var billpay = Get(id);
            if (billpay == null)
            {
                return false;
            }
            else
            {
                billpay.BillPayStatus = BillPayStatus.ReadyToProcess;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
