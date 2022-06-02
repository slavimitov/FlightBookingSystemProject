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
            if (!data.Airports.Any(a => a.IataCode == origin.ToUpper()) || !data.Airports.Any(a => a.IataCode == destination.ToUpper()))
            {
                throw new Exception("Origin or destination does not exist!");
            }
            var airportDestination = data.Airports.FirstOrDefault(x => x.IataCode == destination);
            var airportOrigin = data.Airports.FirstOrDefault(x => x.IataCode == origin);
            var flight = new Flight
            {
                DestinationName = airportDestination.City,
                OriginName = airportOrigin.City,
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

        public Flight GetFlightDetailsForEdit(int flightId)
        {
            return data.Flights.FirstOrDefault(f => f.Id == flightId);      
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

        public bool Edit(int flightId, string origin, string destination, string returnDate, string departureDate, decimal price, string DestinationImageUrl)
        {
            var flight = this.data.Flights.Find(flightId);

            if (flight == null)
            {
                return false;
            }

            flight.OriginIata = origin;
            flight.DestinationIata = destination;
            flight.ReturnDate = DateTime.Parse(returnDate);
            flight.DepartureDate = DateTime.Parse(departureDate);
            flight.Price = price;
            flight.DestinationImageUrl = DestinationImageUrl; 

            this.data.SaveChanges();

            return true;
        }
    }
}
