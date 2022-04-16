using FlightBookingSystemProject.Data;

namespace FlightBookingSystemProject.Services.Travelogues
{
    public class TravelogueService : ITravelogueService
    {
        private readonly FlightBookingDbContext data;
        public TravelogueService(FlightBookingDbContext data)
        {
            this.data = data;
        }

        public void Add(string userId, string title, string subtitle, string content, string destinationImageUrl, string seconImageUrl, string topic, string destination, string email)
        {
            data.Travelogues.Add(new Travelogue
            {
                UserId = userId,
                Title = title,
                Subtitle = subtitle,
                Topic = topic,
                Destination = destination,
                DestinationImageUrl = destinationImageUrl,
                SecondImageUrl = seconImageUrl,
                Email = email,
                Content = content,
            });
            data.SaveChanges();
        }

        public List<Travelogue> GetAll()
        {
            return data.Travelogues.ToList();
        }

        public Travelogue GetById(int travelogueId)
        {
            return data.Travelogues.Find(travelogueId);
        }
    }
}
