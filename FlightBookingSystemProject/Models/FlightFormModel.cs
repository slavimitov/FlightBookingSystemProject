using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Models
{
    public class FlightFormModel
    {
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public string Origin { get; set; }

        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public string Destination { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ReturnDate { get; set; }

        [Required]
        public string DepartureDate { get; set; }

        [Required]
        public string DestinationImageUrl { get; set; }
    }
}
