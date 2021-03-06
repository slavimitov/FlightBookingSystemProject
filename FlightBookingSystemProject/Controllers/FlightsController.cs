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
    public class FlightsController : Controller
    {
        private readonly IFlightService flights;
        private readonly ISeatService seats;
        private readonly IAirlineService airlines;

        public FlightsController(IFlightService flights, ISeatService seats, IAirlineService airlines)
        {            
            this.flights = flights;
            this.seats = seats;
            this.airlines = airlines;
        }
        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.Id();

            if (airlines.IsAirline(userId) == false || User.IsAdmin() == true)
            {
                return BadRequest();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(FlightFormModel flight)
        {
            if (!ModelState.IsValid)
            {
                return View(flight);
            }

            var userId = this.User.Id();

            if (airlines.IsAirline(userId) == false)
            {
                return BadRequest();
            }
            
            var flightId = this.flights.Add(
                flight.Origin,
                flight.Destination,
                flight.ReturnDate,
                flight.DepartureDate,
                flight.Price,
                flight.DestinationImageUrl,
                userId);

            this.seats.CreateSeats(flightId);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult All()
        {
            var query = this.flights.GetAll();

            var flights = query
                .Select(x => new AllFlightsViewModel
                {
                    DestinationName = x.DestinationName,
                    OriginName = x.OriginName,
                    Origin = x.OriginIata,
                    Destination = x.DestinationIata,
                    DepartureDate = x.DepartureDate.ToString("MM/dd/yyyy"),
                    ReturnDate = x.ReturnDate.ToString("MM/dd/yyyy"),
                    Price = x.Price,
                    DestinationImageUrl = x.DestinationImageUrl,
                    FlightId = x.Id
                }).ToList();
            return View(flights);
        }

        public IActionResult Search(string origin, string destination, DateTime departureDate, DateTime returnDate, string travellers)
        {
            var query = flights.GetFiltered(origin, destination, departureDate, returnDate, travellers);

            var queryModel = new QueryModel();
            queryModel.Flights = query
                .Select(x => new AllFlightsViewModel
                {
                    DestinationName = x.DestinationName,
                    OriginName = x.OriginName,
                    Origin = x.OriginIata,
                    Destination = x.DestinationIata,
                    DepartureDate = x.DepartureDate.ToString("MM/dd/yyyy"),
                    ReturnDate = x.ReturnDate.ToString("MM/dd/yyyy"),
                    Price = x.Price,
                    DestinationImageUrl = x.DestinationImageUrl,
                    FlightId = x.Id
                }).ToList();

            return View(queryModel);
        }
    }
}
