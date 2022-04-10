using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Data
{
    public class Airport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string IataCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }
    }
}
