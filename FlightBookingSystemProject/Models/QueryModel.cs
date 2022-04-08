namespace FlightBookingSystemProject.Models
{
    public class QueryModel
    {
        public IEnumerable<AllFlightsViewModel> Flights { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public string Travellers { get; set; }
    }
}
