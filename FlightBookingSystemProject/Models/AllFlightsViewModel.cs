namespace FlightBookingSystemProject.Models
{
    public class AllFlightsViewModel
    {
        public string DestinationName { get; set; }
        public string OriginName { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public string ReturnDate { get; set; }
        public string DepartureDate { get; set; }
        public string DestinationImageUrl { get; set; }
        public int FlightId { get; set; }
    }
}
