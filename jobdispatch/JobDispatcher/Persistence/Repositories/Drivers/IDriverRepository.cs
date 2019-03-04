using JobDispatcher.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Persistence.Repositories.Drivers
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> GetAll(DriverStatus driverStatus);
    }
}
