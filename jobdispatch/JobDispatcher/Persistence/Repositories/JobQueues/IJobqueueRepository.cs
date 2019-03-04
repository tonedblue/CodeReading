using JobDispatcher.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Persistence.Repositories.JobQueues
{
    public interface IJobQueueRepository
    {
        IEnumerable<JobQueue> GetAll(JobQueueStatus jobQueueStatus);
        int Update(JobQueue jobQueue);
        int Add(JobQueue jobQueue);
        int Add(IEnumerable<JobQueue> jobQueue);
    }
}
