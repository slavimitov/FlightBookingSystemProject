using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystemProject.Controllers
{
    public class FlightsController : Controller
    {
        private readonly FlightBookingDbContext data;

        public FlightsController(FlightBookingDbContext data)
        {
            this.data = data;
        }
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(FlightFormModel flight)
        {
            if (!ModelState.IsValid)
            {
                return View(flight);
            }

            var flightTemp = new Flight
            {
                Origin = flight.Origin,
                Destination = flight.Destination,
                ReturnDate = DateTime.Parse(flight.ReturnDate),
                DepartureDate = DateTime.Parse(flight.DepartureDate),
                Price = flight.Price,
                AirlineId = 1,
                DestinationImageUrl = flight.DestinationImageUrl

            };
            this.data.Flights.Add(flightTemp);
            data.SaveChanges();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 65; j <= 70; j++)
                {
                    this.data.Seats.Add(new Seat
                    {
                        Initials = i.ToString() + ((char)j).ToString(),
                        IsBooked = false,
                        FlightId = flightTemp.Id
                    });
                }
            }
            data.SaveChanges();

            return Redirect("/");
        }

        public IActionResult All()
        {
            var query = data.Flights.ToList();

            var flights = query
                .Select(x => new AllFlightsViewModel
                {
                    Origin = x.Origin,
                    Destination = x.Destination,
                    DepartureDate = x.DepartureDate.ToString(),
                    ReturnDate = x.ReturnDate.ToString(),
                    Price = x.Price,
                    DestinationImageUrl = x.DestinationImageUrl,
                    FlightId = x.Id
                }).ToList();

            return View(flights);
        }
    }
}
