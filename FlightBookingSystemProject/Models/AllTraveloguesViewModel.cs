using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Models
{
    public class AllTraveloguesViewModel
    {
        public string Destination { get; set; }

        public string DestinationImageUrl { get; set; }

        public string Title { get; set; }

        public int travelogueId { get; set; }
    }
}
