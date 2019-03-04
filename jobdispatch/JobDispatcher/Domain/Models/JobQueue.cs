using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Domain.Models
{
    public class JobQueue
    {
        public int JobId { get; set; }
        public int OrderId { get; set; }
        public int DriverId { get; set; }
        public Customer Customer { get; set; }
        public JobQueueStatus JobQueueStatus { get; set; }
        public string AvailableDrivers { get; set; }
        public Guid TrackingCode { get; set; }
        public DateTime? JobStartTime { get; set; }
        public DateTime? PackagePickedUpTime { get; set; }
        public DateTime? JobCompleteTime { get; set; }
        public DateTime? DriverSearchStartTime { get; set; }

        public string ToJsonString()
        {
            throw new NotImplementedException();
        }
    }
}
