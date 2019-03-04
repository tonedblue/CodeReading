using JobDispatcher.Domain.Models;
using System;
using System.Collections.Generic;

namespace JobDispatcher.Persistance.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetAll(OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }

        public int Add(IEnumerable<JobQueue> jobQueue)
        {
            throw new NotImplementedException();
        }
    }
}
