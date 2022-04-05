namespace FlightBookingSystemProject.Data
{
    public class Seat
    {
        public int Id { get; set; }
        public string Initials { get; set; }
        public bool IsBooked { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
