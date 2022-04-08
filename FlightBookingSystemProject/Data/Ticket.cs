using Microsoft.AspNetCore.Identity;

namespace FlightBookingSystemProject.Data
{
    public class Ticket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public int SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
