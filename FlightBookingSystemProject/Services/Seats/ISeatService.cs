using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;

namespace FlightBookingSystemProject.Services.Seats
{
    public interface ISeatService
    {
        void CreateSeats(int flightId);
        void BookSeats(IFormCollection formCollection, int flightId, string userId);
        List<Seat> GetSeats(int flightId);
        List<Ticket> GetBookedSeats(string userId);
    }
}
