using System.Device.Location;

namespace JobDispatcher.Domain.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int VehicleTypeId { get; set; }
        public DriverStatus DriverStatus { get; set; }
        public double Score { get; private set; }

        public int CompareTo(Driver driver)
        {
            if (this.Score == driver.Score)
            {
                return this.DriverId.CompareTo(driver.DriverId);
            }

            return driver.Score.CompareTo(this.Score);
        }

        /// <summary>
        /// Distance from job in KM
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public void setScore(double latitude, double longitude)
        {
            var sCoord = new GeoCoordinate(latitude, longitude);
            var eCoord = new GeoCoordinate(this.Latitude, this.Longitude);

            this.Score = sCoord.GetDistanceTo(eCoord) / 1000;
        }
    }
}
