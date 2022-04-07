using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Models
{
    public class BecomeAirlineFormModel
    {
        [Required]
        public string Name { get; set; }
    }
}
