using Microsoft.AspNetCore.Mvc;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Services.Flights;

namespace FlightBookingSystemProject.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {    
            return View();
        }


    }
}
