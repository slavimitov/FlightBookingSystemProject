using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Flights;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FlightBookingSystemProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlightService flights;

        public HomeController(ILogger<HomeController> logger, IFlightService flights)
        {
            this.flights = flights;
            _logger = logger;     
        }

        public IActionResult Index()
        {
            var query = this.flights.GetAll();
            
            var countOfItems = query.Count % 2 == 0 ? query.Count : query.Count - 1;          
           
            var flights = query
                .Select(x => new AllFlightsViewModel
                {
                    Origin = x.OriginIata,
                    Destination = x.DestinationIata,
                    DepartureDate = x.DepartureDate.ToString("MM/dd/yyyy"),
                    ReturnDate = x.ReturnDate.ToString("MM/dd/yyyy"),
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