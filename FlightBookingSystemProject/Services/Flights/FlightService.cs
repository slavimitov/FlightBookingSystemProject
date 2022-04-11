using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;

namespace FlightBookingSystemProject.Services.Flights
{
    public class FlightService:IFlightService
    {
        private readonly FlightBookingDbContext data;
        public FlightService(FlightBookingDbContext data)
        {
            this.data = data;
        }
        public int Add(string origin, string destination, string returnDate, string departureDate, decimal price, string DestinationImageUrl, string userId)
        {
            var flight = new Flight
            {
                OriginIata = origin,
                DestinationIata = destination,
                ReturnDate = DateTime.Parse(returnDate),
                DepartureDate = DateTime.Parse(departureDate),
                Price = price,
                AirlineId = data.Airlines.FirstOrDefault(a => a.UserId == userId).Id,
                DestinationImageUrl = DestinationImageUrl
            };

            this.data.Flights.Add(flight);
            this.data.SaveChanges();
            return flight.Id;
        }

        public void DeleteFlight(int flightId)
        {
            var flight = data.Flights.FirstOrDefault(f => f.Id == flightId);
            data.Flights.Remove(flight);
            data.SaveChanges();
        }

        public List<Flight> GetAll()
        {
            return data.Flights.ToList();
        }

        public List<Flight> GetFiltered(string origin, string destination, DateTime departureDate, DateTime returnDate, string travellers)
        {
            List<Flight> query = data.Flights.ToList();


            if (!string.IsNullOrEmpty(origin))
            {
                query = query.Where(f => f.OriginIata == origin).ToList();
            }
            if (!string.IsNullOrEmpty(destination))
            {
                query = query.Where(f => f.DestinationIata == destination).ToList();
            }
            if (departureDate != new DateTime(0001, 01, 01))
            {
                query = query.Where(f => f.DepartureDate == departureDate).ToList();
            }
            if (returnDate != new DateTime(0001, 01, 01))
            {
                query = query.Where(f => f.ReturnDate == returnDate).ToList();
            }
            if (!string.IsNullOrEmpty(travellers))
            {
                query = query.Where(f => data.Seats.Where(s => f.Id == s.FlightId).Where(s => s.IsBooked == false).Count() >= int.Parse(travellers)).ToList();
            }

            return query;
        }
    }
}
