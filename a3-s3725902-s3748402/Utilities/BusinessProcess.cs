using NwbaSystem.Data;
using NwbaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaSystem.Utilities
{
    public class BusinessProcess
    {
        //Check
        public const decimal CHECK_MIN_ALLOWED_BALANCE = 200;
        decimal minBalance = 0;
        public const decimal SAVINGS_MIN_ALLOWED_BALANCE = 0;
        public const decimal CHECK_WITHDRAW_FEE = 0.10m;
        public const decimal CHECK_TRANSFER_FEE = 0.20m;
        public void OneTimeProcess(BillPay paymentToProcess, NwbaContext _context)
        {
            if(paymentToProcess.ScheduleDate <= DateTime.Now )
            {
                var sourceAccount = _context.Accounts.FirstOrDefault(x => x.AccountNumber == paymentToProcess.AccountNumber);
                if (sourceAccount != null)
                {
                    if (sourceAccount.AccountType.Equals(AccountType.Saving))
                    {
                        minBalance = SAVINGS_MIN_ALLOWED_BALANCE;
                    }
                    else
                    {
                        minBalance = CHECK_MIN_ALLOWED_BALANCE;
                    }

                    if ((sourceAccount.Balance - paymentToProcess.Amount) > minBalance)
                    {
                        var transactionsOfCustomer = _context.Transactions.Where(x => x.AccountNumber == paymentToProcess.AccountNumber);
                        if (transactionsOfCustomer != null)
                        {
                            //transaction
                            var transaction = new Transaction();
                            transaction.TransactionType = TransactionType.BillPay;
                            transaction.AccountNumber = paymentToProcess.AccountNumber;
                            transaction.DestinationAccountNumber = paymentToProcess.PayeeID;
                            transaction.Amount = paymentToProcess.Amount;
                            transaction.TransactionTimeUtc = paymentToProcess.ScheduleDate;
                            transaction.Comment = "Schedule Payment";
                            transaction.ModifyDate = DateTime.Now;

                            _context.Transactions.Add(transaction);
                            paymentToProcess.ModifyDate = DateTime.Now;
                            paymentToProcess.BillPayStatus = BillPayStatus.Success;
                            //reduce balance in source 
                            sourceAccount.Balance = sourceAccount.Balance - paymentToProcess.Amount;
                        }
                    }
                    else
                    {
                        paymentToProcess.ModifyDate = DateTime.Now;
                        paymentToProcess.BillPayStatus = BillPayStatus.Failed;
                    }
                }
            }
        }

        public void MonthlyProcess(BillPay paymentToProcess, NwbaContext _context)
        {
           // TODO:
        }

        public void QuartelyProcess(BillPay paymentToProcess, NwbaContext _context)
        {
            // TODO:
        }

        public void AnnualProcess(BillPay paymentToProcess, NwbaContext _context)
        {
            // TODO:
        }

    }
}
