using JobDispatcher.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Persistance.Repositories.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll(OrderStatus orderStatus);
        int Add(IEnumerable<JobQueue> jobQueue);
    }
}
