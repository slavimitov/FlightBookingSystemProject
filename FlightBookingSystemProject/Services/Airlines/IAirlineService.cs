using FlightBookingSystemProject.Models;

namespace FlightBookingSystemProject.Services.Airlines
{
    public interface IAirlineService
    {
        bool IsAirline(string userId);
        void CreateAirline(string name, string userId);
        string GetAirlineName(int airlineId);
    }
}
