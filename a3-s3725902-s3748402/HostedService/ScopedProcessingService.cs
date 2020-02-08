using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NwbaSystem.Data;
using NwbaSystem.Models;
using NwbaSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NwbaSystem.HostedService
{
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly NwbaContext _context;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, NwbaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);

                BusinessProcess businessProcess = new BusinessProcess();

                if (_context != null)
                {
                    // var x = _context.Accounts.Count();
                    var paymentsToProcess = _context.BillPays.Where(x => x.BillPayStatus == BillPayStatus.ReadyToProcess);
                    foreach (var paymentToProcess in paymentsToProcess)
                    {
                        switch (paymentToProcess.Period)
                        {
                            case "One Time":
                                businessProcess.OneTimeProcess(paymentToProcess, _context);
                                break;
                            case "Monthly":
                                // process this tx
                                businessProcess.OneTimeProcess(paymentToProcess, _context);
                                // schedule next tx
                                if (paymentToProcess.BillPayStatus == BillPayStatus.Success) {
                                    paymentToProcess.ScheduleDate.AddMonths(1); 
                                    paymentToProcess.BillPayStatus = BillPayStatus.ReadyToProcess;
                                }
                                break;
                            case "Quarterly":
                                // process this tx
                                businessProcess.OneTimeProcess(paymentToProcess, _context);
                                // schedule next tx
                                if (paymentToProcess.BillPayStatus == BillPayStatus.Success)
                                {
                                    paymentToProcess.ScheduleDate.AddMonths(3);
                                    paymentToProcess.BillPayStatus = BillPayStatus.ReadyToProcess;
                                }
                                break;
                            case "Annually": //purposely schedule for 30s for demostration purposes
                                // process this tx
                                businessProcess.OneTimeProcess(paymentToProcess, _context);
                                // schedule next tx
                                if (paymentToProcess.BillPayStatus == BillPayStatus.Success)
                                {
                                    paymentToProcess.ScheduleDate.AddMonths(12);
                                    paymentToProcess.BillPayStatus = BillPayStatus.ReadyToProcess;
                                }
                                break;

                            default:

                                break;

                        }
                    }
                    await _context.SaveChangesAsync();
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
