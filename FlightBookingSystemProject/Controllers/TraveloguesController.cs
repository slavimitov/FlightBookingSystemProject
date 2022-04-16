using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Infastructure;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Services.Seats;
using FlightBookingSystemProject.Services.Travelogues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystemProject.Controllers
{
    public class TraveloguesController : Controller
    {
        private readonly ITravelogueService travelogues;
        private readonly IAirlineService airlines;

        public TraveloguesController(ITravelogueService travelogues, IAirlineService airlines)
        {
            this.travelogues = travelogues;
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

            travelogues.Add(
                userId,
                travelogue.Title,
                travelogue.Subtitle,
                travelogue.Content,
                travelogue.DestinationImageUrl,
                travelogue.SecondImageUrl,
                travelogue.Topic,
                travelogue.Destination,
                travelogue.Email);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult All()
        {
            var query = this.travelogues.GetAll();
            var travelogues = query.Select(t => new AllTraveloguesViewModel
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
            var travelogue = travelogues.GetById(travelogueId);
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
