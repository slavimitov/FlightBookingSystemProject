using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Infastructure;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Services.Seats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystemProject.Controllers
{
    public class TraveloguesController : Controller
    {
        private readonly FlightBookingDbContext data;
        private readonly IAirlineService airlines;

        public TraveloguesController(FlightBookingDbContext data, IAirlineService airlines)
        {
            this.data = data;
            this.airlines = airlines;
        }
        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.Id();

            if (airlines.IsAirline(userId) == true || User.IsAdmin() == true)
            {
                return BadRequest();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(TravelogueFormModel travelogue)
        {
            if (!ModelState.IsValid)
            {
                return View(travelogue);
            }

            var userId = this.User.Id();

            if (airlines.IsAirline(userId) == true)
            {
                return BadRequest();
            }

            this.data.Travelogues.Add(new Travelogue
            {
                UserId = userId,
                Title = travelogue.Title,
                Subtitle = travelogue.Subtitle,
                Topic = travelogue.Topic,
                Destination = travelogue.Destination,
                DestinationImageUrl = travelogue.DestinationImageUrl,
                SecondImageUrl = travelogue.SecondImageUrl,
                Email = travelogue.Email,
                Content = travelogue.Content,
            });
            data.SaveChanges();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult All()
        {
            var travelogues = data.Travelogues.Select(t => new AllTraveloguesViewModel
            {
                Title = t.Title,
                Destination = t.Destination,
                DestinationImageUrl = t.DestinationImageUrl,
                travelogueId = t.Id
            }).ToList();
            return View(travelogues);
        }

        public IActionResult Details(int travelogueId)
        {
            var travelogue = data.Travelogues.Find(travelogueId);
            var model = new TravelogueDetailsViewModel
            {
                Topic = travelogue.Topic,
                Destination = travelogue.Destination,
                Title = travelogue.Title,
                Subtitle = travelogue.Subtitle,
                Email = travelogue.Email,
                DestinationImageUrl = travelogue.DestinationImageUrl,
                SecondImageUrl = travelogue.SecondImageUrl,
                Content = travelogue.Content
            };
            return View(model);
        }
    }
}
