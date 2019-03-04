using JobDispatcher.Domain.Models;
using JobDispatcher.Persistance.Repositories.Orders;
using JobDispatcher.Persistence.Repositories.JobQueues;
using System;
using System.Linq;

namespace JobDispatcher.Application.Usecase.QueueOrders
{
    public class QueueOrdersHandler
    {
        readonly IOrderRepository orderRepository;
        readonly IJobQueueRepository jobqueueRepository;

        public QueueOrdersHandler(IOrderRepository orderRepository, IJobQueueRepository jobqueueRepository)
        {
            this.orderRepository = orderRepository;
            this.jobqueueRepository = jobqueueRepository;
        }

        public int Execute()
        {
            Console.WriteLine("Queue Orders");
            var orders = orderRepository.GetAll(OrderStatus.Unprocessed);
            Console.WriteLine("Get all orders");

            if (orders == null || !orders.Any())
            {
                Console.WriteLine("Orders empty");
                return 0;
            }

            var jobQueue = orders.Select(
                item => new JobQueue
                {
                    JobQueueStatus = (int)JobQueueStatus.jobQueued,
                    OrderId = item.OrderID
                }).ToList();

            Console.WriteLine("Jobqueue List created");

            try
            {
                return jobqueueRepository.Add(jobQueue);
            }
            catch
            {
                return 0;
            }
        }
    }
}
