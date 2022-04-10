using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;

namespace FlightBookingSystemProject.Services.Airlines
{
    public class AirlineService : IAirlineService
    {
        private readonly FlightBookingDbContext data;
        public AirlineService(FlightBookingDbContext data)
        {
            this.data = data;
        }

        public void CreateAirline(string name, string userId)
        {
            var airlineData = new Airline
            {
                Name = name,
                UserId = userId
            };

            this.data.Airlines.Add(airlineData);
            this.data.SaveChanges();
        }

        public string GetAirlineName(int flightId)
        {
            return data.Airlines.FirstOrDefault(a => a.Id == flightId).Name;
        }

        public bool IsAirline(string userId)
        {
            return this.data.Airlines.Any(d => d.UserId == userId);
        }
    }
}
