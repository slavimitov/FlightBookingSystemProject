using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Data
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Initials { get; set; }

        [Required]
        public bool IsBooked { get; set; }

        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
