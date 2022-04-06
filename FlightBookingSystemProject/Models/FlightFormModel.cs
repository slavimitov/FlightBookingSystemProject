namespace FlightBookingSystemProject.Models
{
    public class FlightFormModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public string TakeOffTime { get; set; }
        public string DepartureTime { get; set; }
        public string DestinationImageUrl { get; set; }
    }
}
