using JobDispatcher.Domain.Models;
using JobDispatcher.Persistence.Repositories.CreateJob;
using JobDispatcher.Persistence.Repositories.Drivers;
using JobDispatcher.Persistence.Repositories.JobQueues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobDispatcher.Application.Usecase.ProcessQueuedJobs
{
    public class ProcessQueuedJobsHandler
    {
        readonly IDriverRepository driverRepository;
        readonly IJobQueueRepository jobQueueRepository;
        readonly ICreateJobRepository createJobRepository;

        public ProcessQueuedJobsHandler(IDriverRepository driverRepository, IJobQueueRepository jobQueueRepository, ICreateJobRepository createJobRepository)
        {
            this.driverRepository = driverRepository;
            this.jobQueueRepository = jobQueueRepository;
            this.createJobRepository = createJobRepository;
        }

        public int Execute(double maxDistanceKm, int driverLimit)
        {
            var queuedJobs = GetJobs(maxDistanceKm, driverLimit);
            return SendJobs(queuedJobs);
        }

        private IEnumerable<JobQueue> GetJobs(double maxDistanceKm, int driverLimit)
        {
            Console.WriteLine("Process Orders");
            //Get all queued jobs
            var queuedJobs = jobQueueRepository.GetAll(JobQueueStatus.jobQueued);
            Console.WriteLine("Get Queued jobs");

            if (queuedJobs == null || !queuedJobs.Any())
            {
                Console.WriteLine("No jobs to process");
                return default(List<JobQueue>);
            }

            List<JobQueue> validJobs = new List<JobQueue>();

            //Find drivers are waiting for jobs and are close to it
            foreach (var queuedJob in queuedJobs)
            {
                var validDrivers = driverRepository.GetAll(DriverStatus.Waiting);
                var driverCandidates = new List<Driver>();

                if (validDrivers == null || !validDrivers.Any())
                {
                    queuedJob.JobQueueStatus = JobQueueStatus.noDriverFound;
                    jobQueueRepository.Update(queuedJob);
                    Console.WriteLine("No drivers found");
                    continue;
                }

                foreach (var validDriver in validDrivers)
                {
                    var driver = new Driver
                    {
                        DriverId = validDriver.DriverId,
                        Latitude = validDriver.Latitude,
                        Longitude = validDriver.Longitude
                    };

                    driver.setScore(queuedJob.Customer.Address.Latitude, queuedJob.Customer.Address.Longitude);
                    driverCandidates.Add(driver);
                }

                var closestCandidates = driverCandidates.Where(x => x.Score < maxDistanceKm).OrderBy(x => x.Score).Take(driverLimit);

                if (!closestCandidates.Any())
                {
                    queuedJob.JobQueueStatus = JobQueueStatus.noDriverFound;
                    jobQueueRepository.Update(queuedJob);
                    Console.WriteLine("No drivers found");
                    continue;
                }

                //Send job to closest driver
                //var driverIds = string.Join(",", closestCandidates.Select(item => item.DriverId));
                //queuedJob.AvailableDrivers = driverIds;

                var driverId = closestCandidates.First().DriverId;
                queuedJob.DriverId = driverId;

                validJobs.Add(queuedJob);
            }

            return validJobs;
        }

        private int SendJobs(IEnumerable<JobQueue> queuedJobs)
        {
            Console.WriteLine("Sending jobs");
            int count = 0;

            if (queuedJobs == null || !queuedJobs.Any())
            {
                Console.WriteLine("No jobs to send");
                return 0;
            }

            foreach (JobQueue queuedJob in queuedJobs)
            {
                bool result = createJobRepository.SendJob(queuedJob.ToJsonString(), queuedJob.DriverId);

                if (!result)
                {
                    continue;
                }

                Console.WriteLine($"Job sent to firebase {queuedJob.JobId}");

                queuedJob.DriverSearchStartTime = DateTime.Now;
                queuedJob.JobQueueStatus = JobQueueStatus.searchingDriver;
                Console.WriteLine("Updating Job");
                jobQueueRepository.Update(queuedJob);
                count++;
            }

            return count;
        }

    }
}