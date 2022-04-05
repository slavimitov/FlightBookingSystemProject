namespace FlightBookingSystemProject.Data
{
    public class Airline
    {
        public Airline()
        {
            Flights = new HashSet<Flight>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
