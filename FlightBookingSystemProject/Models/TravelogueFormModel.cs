using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemProject.Models
{
    public class TravelogueFormModel
    {
        [Required]
        public string Destination { get; set; }

        [Required]
        public string DestinationImageUrl { get; set; }

        [Required]
        public string SecondImageUrl { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Subtitle { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Topic { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public string Email { get; set; }
    }
}
