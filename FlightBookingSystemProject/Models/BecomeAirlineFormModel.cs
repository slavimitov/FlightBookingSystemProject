using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Models
{
    public class BecomeAirlineFormModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
