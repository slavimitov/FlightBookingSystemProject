using Microsoft.AspNetCore.Mvc;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Models;

namespace FlightBookingSystemProject.Areas.Admin.Controllers
{
    public class FlightsController : AdminController
    {
        private readonly IFlightService flights;
        public FlightsController(IFlightService flights)
        {
            this.flights = flights;
        }

        public IActionResult Delete(int id)
        {
            return this.View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            flights.DeleteFlight(id);
            return Redirect("/");
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

        public IActionResult Edit(int id)
        {     
            var flight = this.flights.GetFlightDetailsForEdit(id);
            var flightModel = new FlightFormModel
            {
                Origin = flight.OriginIata,
                Destination = flight.DestinationIata,
                ReturnDate = flight.ReturnDate.ToString(),
                DepartureDate = flight.DepartureDate.ToString(),
                Price = flight.Price,
                DestinationImageUrl = flight.DestinationImageUrl
            };

            return View(flightModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, FlightFormModel flight)
        {      
            if (!ModelState.IsValid)
            { 
                return View(flight);
            }

            var edited = this.flights.Edit(id, flight.Origin, flight.Destination, flight.ReturnDate, flight.DepartureDate, flight.Price, flight.DestinationImageUrl);

            if (edited == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All), "Flights");
        }
    }
}
