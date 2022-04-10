using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Infastructure;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystemProject.Controllers
{
    public class AirlinesController : Controller
    {
        private readonly IAirlineService airlines;

        public AirlinesController(IAirlineService airlines)
        {
            this.airlines = airlines;
        }

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeAirlineFormModel airline)
        {
            var userId = this.User.Id();
     
            if (airlines.IsAirline(userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(airline);
            }

            airlines.CreateAirline(airline.Name, userId); ;

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
