using NwbaSystem.Models;
using NwbaSystem.ViewModels;
using System;
using System.Runtime.InteropServices;

namespace ClassLibraryTransaction
{
    public class TransactionBusinessLogic
    {
        public void ProcessTransaction(Account fromAccount, [Optional] Account toAccount, TransactionViewModel transactionViewModel)
        {
            switch (transactionViewModel.TransactionType)
            {
                case ("D"):
                    fromAccount.Balance += transactionViewModel.Amount;
                    fromAccount.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Deposit,
                            Amount = transactionViewModel.Amount,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    break;
                case ("W"):
                    fromAccount.Balance -= transactionViewModel.Amount;
                    fromAccount.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Withdraw,
                            Amount = transactionViewModel.Amount,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    break;
                case ("T"):
                    fromAccount.Balance -= transactionViewModel.Amount;
                    toAccount.Balance += transactionViewModel.Amount;
                    fromAccount.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Transfer,
                            Amount = transactionViewModel.Amount,
                            DestinationAccountNumber = transactionViewModel.DestinationAccountNumber,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    toAccount.Transactions.Add(
                        new Transaction
                        {
                            TransactionType = TransactionType.Transfer,
                            Amount = transactionViewModel.Amount,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = transactionViewModel.Comment
                        });
                    break;
            }
        }
    }
}
