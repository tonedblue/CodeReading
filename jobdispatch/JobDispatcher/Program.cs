using JobDispatcher.Application.Usecase.ProcessQueuedJobs;
using JobDispatcher.Application.Usecase.QueueOrders;
using JobDispatcher.Persistance.Repositories.Orders;
using JobDispatcher.Persistence.Repositories.CreateJob;
using JobDispatcher.Persistence.Repositories.Drivers;
using JobDispatcher.Persistence.Repositories.JobQueues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create repositories
            IOrderRepository orderRepository = new OrderRepository();
            IJobQueueRepository jobQueueRepository = new JobQueueRepository();
            IDriverRepository driverRepository = new DriverRepository();
            ICreateJobRepository createJobRepository = new FirebaseJobRepository();
            
            //System settings TODO
            double maxDistanceKm = 50;
            int maxDrivers = 3;

            //Process orders unproccesed orders
            var queueOrdersHandler = new QueueOrdersHandler(orderRepository, jobQueueRepository);
            int results = queueOrdersHandler.Execute();
            Console.WriteLine($"Orders Queued {results}");

            //Send processed orders to available drivers
            var processQueuedJobsHandlder = new ProcessQueuedJobsHandler(driverRepository, jobQueueRepository, createJobRepository);
            results = processQueuedJobsHandlder.Execute(maxDistanceKm, maxDrivers);
            Console.WriteLine($"Jobs sent to drivers {results}");
        }
    }
}
