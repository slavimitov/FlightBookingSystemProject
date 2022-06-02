using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Flights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace FlightBookingSystemProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlightService flights;
        private readonly IMemoryCache cache;

        public HomeController(ILogger<HomeController> logger, IFlightService flights, IMemoryCache cache)
        {
            this.flights = flights;
            _logger = logger;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestFlights = this.cache.Get<List<AllFlightsViewModel>>("LatestFlightsCacheKey");
            if (latestFlights == null)
            {
                var query = this.flights.GetAll();

                var countOfItems = query.Count % 2 == 0 ? query.Count : query.Count - 1;

                 latestFlights = query
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
                    })
                    .Take(countOfItems)
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(20));

                this.cache.Set("LatestFlightsCacheKey", latestFlights, cacheOptions);
            }
            return View(latestFlights);
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