using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Data
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int? SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
