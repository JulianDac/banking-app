using NwbaApi.Data;
using NwbaApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Models.DataManager
{
    public class BillPayManager : IDataRepository<BillPay, int>
    {
        private readonly NwbaContext _context;

        public BillPayManager(NwbaContext context)
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
