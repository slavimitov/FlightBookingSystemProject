using Microsoft.AspNetCore.Mvc;
using FlightBookingSystemProject.Data;

namespace FlightBookingSystemProject.Areas.Admin.Controllers
{
    public class FlightsController : AdminController
    {
        private readonly FlightBookingDbContext data;
        public FlightsController(FlightBookingDbContext data)
        {
            this.data = data; 
        }

        public IActionResult Delete(int id)
        {
          
            return this.View();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var flight = data.Flights.FirstOrDefault(f => f.Id == id);
            data.Flights.Remove(flight);
            data.SaveChanges();
            return Redirect("/");
        }
    }
}
