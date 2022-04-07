using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Infastructure;
using FlightBookingSystemProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystemProject.Controllers
{
    public class AirlinesController : Controller
    {
        private readonly FlightBookingDbContext data;

        public AirlinesController(FlightBookingDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeAirlineFormModel airline)
        {
            var userId = this.User.Id();

            var userIdAlreadyDealer = this.data
                .Airlines
                .Any(d => d.UserId == userId);

            if (userIdAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(airline);
            }

            var airlineData = new Airline
            {
                Name = airline.Name,
                UserId = userId
            };

            this.data.Airlines.Add(airlineData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
