using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Persistence.Repositories.CreateJob
{
    public interface ICreateJobRepository
    {
        bool SendJob(string JobDetailsjson, int driverId);
    }
}
