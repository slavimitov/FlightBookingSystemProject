using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;

namespace FlightBookingSystemProject.Services.Flights
{
    public interface IFlightService
    {
        int Add(string origin, string destination, string returnDate, string departureDate, decimal price, string DestinationImageUrl, string userId);
        List<Flight> GetAll();
        List<Flight> GetFiltered(string origin, string destination, DateTime departureDate, DateTime returnDate, string travellers);
        void DeleteFlight(int flightId);
        Flight GetFlightDetailsForEdit(int flightId);
        bool Edit(int flightId, string origin, string destination, string returnDate, string departureDate, decimal price, string DestinationImageUrl);
    }
}
