using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Infastructure;
using FlightBookingSystemProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FlightBookingSystemProject.Controllers
{
    public class SeatsController : Controller
    {
        private readonly FlightBookingDbContext data;

        public SeatsController(FlightBookingDbContext data)
        {
            this.data = data;
        }
        public IActionResult Book(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var query = data.Seats.Where(s => s.FlightId == id).ToList();
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

        [HttpPost]
        public IActionResult Book(IFormCollection formCollection, int id)
        {
            var seats = data.Seats.Where(s => s.FlightId == id).ToList();
            var firstSeat = seats.First();
            var lastSeat = seats.Last();
            for (int i = firstSeat.Id; i <= lastSeat.Id; i++)
            {

                if (!string.IsNullOrEmpty(formCollection[$"{i}"]))
                {
                    var seat = data.Seats.FirstOrDefault(s => s.FlightId == id && s.Id == int.Parse(formCollection[$"{i}"]));
                    seat.IsBooked = true;
                    data.Tickets.Add(new Ticket
                    {
                        UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                        SeatId = seat.Id,
                        FlightId = seat.FlightId

                    });
                    data.SaveChanges();
                }
            }
            return Redirect("/");
        }

        public IActionResult MyBookedSeats()
        {

            var query = data.Tickets.Where(t => t.UserId == this.User.Id()).ToList();

            var tickets = query
                .Select(x => new TicketViewModel
                {
                    
                    AirlineName = data.Airlines.FirstOrDefault(a => a.Id == data.Flights.FirstOrDefault(f => f.Id == x.FlightId).AirlineId).Name,
                    Destination = data.Flights.FirstOrDefault(f => f.Id == x.FlightId).Destination,
                    Origin = data.Flights.FirstOrDefault(f => f.Id == x.FlightId).Origin,
                    DepartureDate = data.Flights.FirstOrDefault(f => f.Id == x.FlightId).DepartureDate.ToString(),
                    Email = this.User.FindFirst(ClaimTypes.Email).Value,
                    FlightId = x.FlightId,
                    SeatInitials = data.Seats.FirstOrDefault(s => s.Id == x.SeatId).Initials
                }).ToList();

            return View(tickets);
        }
    }
}
