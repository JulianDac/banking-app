using NwbaApi.Data;
using NwbaApi.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Models.DataManager
{
    public class TransactionManager : IDataRepository<Transaction, int>
    {
        private readonly NwbaContext _context;

        public TransactionManager(NwbaContext context)
        {
            _context = context;
        }

        public Transaction Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        // Get list of transactions for all the account numbers passed in and filter based on date 
        public IEnumerable<Transaction> GetAccountTransactions(List<int> accountNumbers, DateTime? from, DateTime? to)
        {
 
            if (from.HasValue && to.HasValue)
            {
                var transactions = _context.Transactions.Where(x => accountNumbers.Contains(x.AccountNumber) && (x.TransactionTimeUtc.Date >= from.Value.Date && x.TransactionTimeUtc.Date <= to.Value.Date)).ToList();
                foreach (var tran in transactions)
                {
                    tran.Account = null;
                }
                return transactions;
            }
            else if (from.HasValue && !to.HasValue)
            {
                var transactions = _context.Transactions.Where(x => accountNumbers.Contains(x.AccountNumber) && (x.TransactionTimeUtc.Date >= from.Value.Date )).ToList();
                foreach (var tran in transactions)
                {
                    tran.Account = null;
                }
                return transactions;
            }
            else if (!from.HasValue && to.HasValue)
            {
                var transactions = _context.Transactions.Where(x => accountNumbers.Contains(x.AccountNumber) && (x.TransactionTimeUtc.Date <= to.Value.Date)).ToList();
                foreach (var tran in transactions)
                {
                    tran.Account = null;
                }
                return transactions;
            }
            else
            {
                var transactions = _context.Transactions.Where(x => accountNumbers.Contains(x.AccountNumber) ).ToList();
                foreach (var tran in transactions)
                {
                    tran.Account = null;
                }
                return transactions;
            }
        }

        public int Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.TransactionID;
        }

        public int Update(int id, Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }

        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();

            return id;
        }
    }
}
