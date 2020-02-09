///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NwbaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Data
{
    public class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new NwbaContext(serviceProvider.GetRequiredService<DbContextOptions<NwbaContext>>());

            // Look for customers.
            if (context.Customers.Any())
                return; // DB has already been seeded.

            const string format = "dd/MM/yyyy hh:mm:ss tt";
            context.Addresses.AddRange(
            new Address
            {
                AddressID = 1001,
                Street = "3 Eugenia Street",
                City = "Doveton",
                State = "Vic",
                PostCode = "3177",
                Phone = "(61)-12340678"
            },
            new Address
            {
                AddressID = 1002,
                Street = "30 McDownald Ave",
                City = "Clyton",
                State = "QLD",
                PostCode = "3172",
                Phone = "(61)-04877954"
            },
            new Address
            {
                AddressID = 1003,
                Street = "123 Fake Street",
                City = "Melbourne",
                State = "VIC",
                PostCode = "3000",
                Phone = "(61)-03676983"
            },
            new Address
            {
                AddressID = 1004,
                Street = "12 Dream Ave",
                City = "Melbourne",
                State = "WA",
                PostCode = "3002",
                Phone = "(61)-034598743"
            },
            new Address
            {
                AddressID = 1005,
                Street = "32 poka Close",
                City = "Melbourne",
                State = "NSW",
                PostCode = "3007",
                Phone = "(61)-45693487"
            },
            new Address
            {
                AddressID = 1006,
                Street = "Unit2 Mac Road",
                City = "Melbourne",
                State = "TAS",
                PostCode = "3006",
                Phone = "(61)-45692383"
            });

            context.Customers.AddRange(
            new Customer
            {
                CustomerID = 2100,
                Name = "Matthew Bolger",
                Tfn = "1234567890",
                AddressID = 1001
            },
            new Customer
            {
                CustomerID = 2200,
                Name = "Rodney Cocker",
                Tfn = "0987654321",
                AddressID = 1002

            },
            new Customer
            {
                CustomerID = 2300,
                Name = "Shekhar Kalra",
                Tfn = "0192837465",
                AddressID = 1003
            });

            context.Payees.AddRange(
            new Payee
            {
                PayeeID = 2001,
                Name = "Telstra",
                AddressID = 1004
            },
            new Payee
            {
                PayeeID = 2002,
                Name = "Origin",
                AddressID = 1005
            },
            new Payee
            {
                PayeeID = 2003,
                Name = "Home Loan",
                AddressID = 1006
            });

            context.Logins.AddRange(
            new Login
            {
                LoginID = "12345678",
                CustomerID = 2100,
                PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                LockFlag = LockFlag.NotLocked,
                FailedAttempts = 0,
                LockTime = null,
                ModifyDate = DateTime.ParseExact("20/01/2020 09:00:00 PM", format, null)
            },
            new Login
            {
                LoginID = "38074569",
                CustomerID = 2200,
                PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04",
                LockFlag = LockFlag.NotLocked,
                FailedAttempts = 0,
                LockTime = null,
                ModifyDate = DateTime.ParseExact("20/01/2020 09:00:00 PM", format, null)
            },
            new Login
            {
                LoginID = "17963428",
                CustomerID = 2300,
                PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE",
                LockFlag = LockFlag.NotLocked,
                FailedAttempts = 0,
                LockTime = null,
                ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
            });

            context.Accounts.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = AccountType.Saving,
                    CustomerID = 2100,
                    Balance = 100,
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = AccountType.Checking,
                    CustomerID = 2100,
                    Balance = 500,
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = AccountType.Saving,
                    CustomerID = 2200,
                    Balance = 500.95m,
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                },
                new Account
                {
                    AccountNumber = 4300,
                    AccountType = AccountType.Checking,
                    CustomerID = 2300,
                    Balance = 1250.50m,
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                });

            const string openingBalance = "Opening balance";
            //  const string format = "dd/MM/yyyy hh:mm:ss tt";
            context.Transactions.AddRange(
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4101,
                    Amount = 500,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null),
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    Amount = 500.95m,
                    Comment = openingBalance,
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null),
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4300,
                    Amount = 1250.50m,
                    Comment = "Opening balance",
                    TransactionTimeUtc = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null),
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null)
                });
            context.BillPays.AddRange(
                new BillPay
                {
                    AccountNumber = 4101,
                    PayeeID = 2001,
                    Amount = 234.90m,
                    ScheduleDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null),
                    Period = "",
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null),
                    BillPayStatus = BillPayStatus.Success
                },
                new BillPay
                {
                    AccountNumber = 4300,
                    PayeeID = 2001,
                    Amount = 130.30m,
                    ScheduleDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null),
                    Period = "",
                    BillPayStatus = BillPayStatus.Success
                },
                new BillPay
                {
                    AccountNumber = 4100,
                    PayeeID = 2001,
                    Amount = 34.25m,
                    ScheduleDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null),
                    Period = "",
                    ModifyDate = DateTime.ParseExact("20/01/2020 09:30:00 PM", format, null),
                    BillPayStatus = BillPayStatus.Success
                });

            context.SaveChanges();
        }
    }
}
