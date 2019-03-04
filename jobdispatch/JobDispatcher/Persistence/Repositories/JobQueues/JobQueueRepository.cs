using JobDispatcher.Domain.Models;
using System;
using System.Collections.Generic;

namespace JobDispatcher.Persistence.Repositories.JobQueues
{
    public class JobQueueRepository : IJobQueueRepository
    {
        public IEnumerable<JobQueue> GetAll(JobQueueStatus jobQueueStatus)
        {
            throw new NotImplementedException();
        }
        public int Update(JobQueue jobQueue)
        {
            throw new NotImplementedException();
        }
        public int Add(JobQueue jobQueue)
        {
            throw new NotImplementedException();
        }
        public int Add(IEnumerable<JobQueue> jobQueue)
        {
            throw new NotImplementedException();
        }
    }
}
