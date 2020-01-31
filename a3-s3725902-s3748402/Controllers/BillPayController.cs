///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-2 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.ViewModels;
using System.Web;
using System.Text;


namespace NwbaSystem.Controllers
{
    public class BillPayController : Controller
    {
        private readonly NwbaContext _context;
        private int customerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public BillPayController(NwbaContext context) => _context = context;


        public async Task<IActionResult> Index()
        {
            // Eager loading.
            var customer = await _context.Customers.Include(x => x.Accounts).
               FirstOrDefaultAsync(x => x.CustomerID == customerID);

            var accountNumbers = customer?.Accounts?.Select(x => x.AccountNumber)?.ToList();

            var schedulePayments = _context.BillPays?.Where(x => accountNumbers.Contains(x.AccountNumber));

            var result = new List<ViewModels.BillPayViewModel>();

            foreach (var schedule in schedulePayments)
            {
                var scheduleModel = new BillPayViewModel();
                scheduleModel.PayeeID = schedule.PayeeID;

                var payee = _context.Payees?.FirstOrDefault(x => x.PayeeID == schedule.PayeeID);
                scheduleModel.BillPayID = schedule.BillPayID;
                scheduleModel.PayeeName = payee.Name;
                scheduleModel.Amount = schedule.Amount;
                scheduleModel.ScheduleDate = schedule.ScheduleDate;
                scheduleModel.Period = schedule.Period;
                scheduleModel.ModifyDate = schedule.ModifyDate;
                scheduleModel.BillPayStatus = schedule.BillPayStatus;

                result.Add(scheduleModel);
            }

            return View(result);
        }

        public async Task<IActionResult> Save()
        {
            // Eager loading.
            var customer = await _context.Customers.Include(x => x.Accounts).
               FirstOrDefaultAsync(x => x.CustomerID == customerID);

            List<SelectListItem> accounts = new List<SelectListItem>();

            foreach (var account in customer?.Accounts)
            {
                accounts.Add(new SelectListItem()
                {
                    Text = account.AccountNumber.ToString(),
                    Value = account.AccountNumber.ToString()

                });
            }

            var billPayers = _context.Payees.ToList();
            List<SelectListItem> payeeList = new List<SelectListItem>();
            foreach (var payee in billPayers)
            {
                payeeList.Add(new SelectListItem()
                {
                    Text = payee.Name,
                    Value = payee.PayeeID.ToString()
                });
            }

            var model = new BillPayViewModel();
            model.Accounts = accounts;
            model.PayeeList = payeeList;
            model.ScheduleDate = DateTime.Today;
            model.PaymentFrequency = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "Monthly", Value = "Monthly"
                },
                new SelectListItem
                {
                    Text = "Quarterly", Value = "Quarterly"
                },
                new SelectListItem
                {
                    Text = "Annually", Value = "Annually"
                },
                 new SelectListItem
                {
                    Text = "One Time", Value = "One Time"
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(BillPayViewModel viewModel)
        {
            var billPay = new BillPay();
            billPay.AccountNumber = Convert.ToInt32(viewModel.SelectedAccount);
            billPay.PayeeID = Convert.ToInt32(viewModel.SelectedPayee);
            billPay.Amount = viewModel.Amount;
            billPay.ScheduleDate = viewModel.ScheduleDate;
            billPay.Period = viewModel.SelectedPaymentFrequency;
            billPay.BillPayStatus = BillPayStatus.Waiting;

            _context.BillPays.Add(billPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.Include(x => x.Accounts).
               FirstOrDefaultAsync(x => x.CustomerID == customerID);

            List<SelectListItem> accounts = new List<SelectListItem>();

            foreach (var account in customer?.Accounts)
            {
                accounts.Add(new SelectListItem()
                {
                    Text = account.AccountNumber.ToString(),
                    Value = account.AccountNumber.ToString()

                });
            }

            var billPayers = _context.Payees.ToList();

            List<SelectListItem> payeeList = new List<SelectListItem>();
            foreach (var payee in billPayers)
            {
                payeeList.Add(new SelectListItem()
                {
                    Text = payee.Name,
                    Value = payee.PayeeID.ToString()
                });
            }

            var model = new BillPayViewModel();
            model.Accounts = accounts;
            model.PayeeList = payeeList;
            model.PaymentFrequency = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "Monthly", Value = "Monthly"
                },
                new SelectListItem
                {
                    Text = "Quarterly", Value = "Quarterly"
                },
                new SelectListItem
                {
                    Text = "Annually", Value = "Annually"
                },
                 new SelectListItem
                {
                    Text = "One Time", Value = "One Time"
                }
            };
            var currentSchedule = _context.BillPays.FirstOrDefault(x => x.BillPayID == id);

            model.Amount = currentSchedule.Amount;
            model.ScheduleDate = currentSchedule.ScheduleDate;
            model.SelectedPaymentFrequency = currentSchedule.Period;
            model.SelectedPayee = currentSchedule.PayeeID.ToString();
            model.BillPayID = currentSchedule.BillPayID;
            model.BillPayStatus = currentSchedule.BillPayStatus;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BillPayViewModel viewModel)
        {
            var billPay = _context.BillPays.FirstOrDefault(x => x.BillPayID == viewModel.BillPayID);
            billPay.AccountNumber = Convert.ToInt32(viewModel.SelectedAccount);
            billPay.PayeeID = Convert.ToInt32(viewModel.SelectedPayee);
            billPay.Amount = viewModel.Amount;
            billPay.ScheduleDate = viewModel.ScheduleDate;
            billPay.Period = viewModel.SelectedPaymentFrequency;
            billPay.ModifyDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}