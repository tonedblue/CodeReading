using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Domain.Models
{
    public enum JobQueueStatus
    {
        jobQueued = 0,
        searchingDriver = 1,
        rejected = 2,
        jobAccepted = 3,
        packagePickedup = 4,
        jobComplete = 5,
        noDriverFound = 6
    }
}
