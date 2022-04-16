using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Models
{
    public class TravelogueDetailsViewModel
    {
        public string Destination { get; set; }

        public string DestinationImageUrl { get; set; }

        public string SecondImageUrl { get; set; }

        public string Title { get; set; }

        public string Topic { get; set; }

        public string Email { get; set; }

        public string Subtitle { get; set; }

        public string Content { get; set; }

    }
}
