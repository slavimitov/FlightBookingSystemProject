using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                    data.SaveChanges();
                }
            }
            return Redirect("/");
        }
    }
}
