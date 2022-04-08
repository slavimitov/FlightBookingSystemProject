using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FlightBookingSystemProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FlightBookingDbContext data;

        public HomeController(ILogger<HomeController> logger, FlightBookingDbContext data)
        {
            _logger = logger;
            this.data = data;
        }

        public IActionResult Index()
        {
            var query = data.Flights.ToList();
            
            var countOfItems = query.Count % 2 == 0 ? query.Count : query.Count - 1;          
           
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
                })
                .Take(countOfItems)
                .ToList();

            return View(flights);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}