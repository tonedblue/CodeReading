using JobDispatcher.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Persistence.Repositories.Drivers
{
    public class DriverRepository : IDriverRepository
    {
        public IEnumerable<Driver> GetAll(DriverStatus driverStatus)
        {
            throw new NotImplementedException();
        }

    }
}
