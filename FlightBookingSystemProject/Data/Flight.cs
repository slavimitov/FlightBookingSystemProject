namespace FlightBookingSystemProject.Data
{
    public class Flight
    {
        public Flight()
        {
            Seats = new HashSet<Seat>();
        }
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public DateTime TakeOffTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int AirlineId { get; set; }
        public string DestinationImageUrl { get; set; }
        public Airline Airline { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
