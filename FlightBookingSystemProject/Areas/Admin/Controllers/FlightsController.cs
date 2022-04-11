using Microsoft.AspNetCore.Mvc;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Services.Flights;

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
    }
}
