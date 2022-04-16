using FlightBookingSystemProject.Data;

namespace FlightBookingSystemProject.Services.Travelogues
{
    public interface ITravelogueService
    {
        void Add(string userId, string title, string subtitle, string content, string destinationImageUrl, string seconImageUrl, string topic, string destination, string email);
        List<Travelogue> GetAll();
        Travelogue GetById(int travelogueId);
    }
}
