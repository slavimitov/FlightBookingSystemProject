using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Data
{
    public class Airline
    {
        public Airline()
        {
            Flights = new HashSet<Flight>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
