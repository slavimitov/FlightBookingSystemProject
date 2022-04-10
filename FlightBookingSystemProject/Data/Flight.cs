using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Data
{
    public class Flight
    {
        public Flight()
        {
            Seats = new HashSet<Seat>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string OriginIata { get; set; }

        [Required]
        [StringLength(3)]
        public string DestinationIata { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public int AirlineId { get; set; }

        [Required]
        public string DestinationImageUrl { get; set; }
        public Airline Airline { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}
