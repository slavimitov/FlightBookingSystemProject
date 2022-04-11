using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Infastructure;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Services.Seats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlightBookingSystemProject.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ISeatService seats;
        private readonly IAirlineService airlines;
        public SeatsController(ISeatService seats, IAirlineService airlines)
        {
            this.seats = seats;
            this.airlines = airlines;
        }

        [Authorize]
        public IActionResult Book(int id)
        {
            var userId = this.User.Id();
            if (airlines.IsAirline(userId) == true)
            {
                return RedirectToAction(nameof(FlightsController.All), "Flights");
            }

            List<SelectListItem> items = new List<SelectListItem>();
            var query = seats.GetSeats(id);
            foreach (var seat in query)
            {
                items.Add(new SelectListItem
                {
                    Text = seat.Initials,
                    Value = seat.Id.ToString(),
                    Disabled = seat.IsBooked
                }) ;
            }

            return View(items);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Book(IFormCollection formCollection, int id)
        {
            var userId = this.User.Id();
            if (airlines.IsAirline(userId) == true)
            {
                return RedirectToAction(nameof(FlightsController.All), "Flights");
            }
            seats.BookSeats(formCollection, id, userId);
            return RedirectToAction(nameof(HomeController.Index), "Home"); ;
        }

        [Authorize]
        public IActionResult MyBookedSeats()
        {
            var userId = this.User.Id();
            if (airlines.IsAirline(userId) == true)
            {
                return RedirectToAction(nameof(FlightsController.All), "Flights");
            }

            var userEmail = this.User.FindFirst(ClaimTypes.Email).Value;

            var query = seats.GetBookedSeats(userId, userEmail);

            if (query.Count == 0)
            {
                throw new ArgumentException("Flight does not exist");
            }
            var tickets = query
                .Select(x => new TicketViewModel
                {
                    AirlineName = airlines.GetAirlineName(x.Flight.AirlineId),
                    Destination = x.Flight.DestinationIata,
                    Origin = x.Flight.OriginIata,
                    DepartureDate = x.Flight.DepartureDate.ToString(),
                    Email = userEmail,
                    FlightId = x.FlightId,
                    SeatInitials = x.Seat.Initials
                }).ToList();

            return View(tickets);
        }
    }
}
