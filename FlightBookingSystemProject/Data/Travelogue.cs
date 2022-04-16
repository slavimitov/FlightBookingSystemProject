using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Data
{
    public class Travelogue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string DestinationImageUrl { get; set; }

        [Required]
        public string SecondImageUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(30)]
        public string Subtitle { get; set; }

        [Required]
        [StringLength(30)]
        public string Topic { get; set; }

        [Required]
        public string Content { get; set; }

        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
