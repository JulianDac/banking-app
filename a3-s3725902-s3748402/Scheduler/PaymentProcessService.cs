using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NwbaSystem.Scheduler
{
    //https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/amp/
   // public class PaymentProcessService : IHostedService
   // {
        // We need to inject the IServiceProvider so we can create 
        // the scoped service, MyDbContext
  //      private readonly IServiceProvider _serviceProvider;
        //public MigratorStartupFilter(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}

        //public async Task StartAsync(CancellationToken cancellationToken)
        //{
        //    // Create a new scope to retrieve scoped services
        //    using (var scope = _seviceProvider.CreateScope())
        //    {
        //        // Get the DbContext instance
        //      //  var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

        //        //Do the migration asynchronously
        //      //  await myDbContext.Database.MigrateAsync();
        //    }
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    // noop
        //    return Task.CompletedTask;
        //}
   // }
}
