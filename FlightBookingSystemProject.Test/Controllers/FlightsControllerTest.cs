using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightBookingSystemProject.Test.Controllers
{
    public class FlightsControllerTest
    {
        private static Mock<IAirlineService> SetupIsAirlineTrue()
        {
            var airlineServiceMock = new Mock<IAirlineService>();

            airlineServiceMock
                .Setup(x => x.IsAirline("1"))
                .Returns(true);
            return airlineServiceMock;
        }
        private static Mock<IAirlineService> SetupIsAirlineFalse()
        {
            var airlineServiceMock = new Mock<IAirlineService>();

            airlineServiceMock
                .Setup(x => x.IsAirline("1"))
                .Returns(false);
            return airlineServiceMock;
        }
        private static ClaimsPrincipal MakeClaim()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "1"),
                                        new Claim(ClaimTypes.Name, "ad@com")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            return user;
        }


        [Test]
        public void AddShouldReturnView()
        {

            //Arrange
            var flightsController = new FlightsController(null, null, SetupIsAirlineTrue().Object);
            ClaimsPrincipal user = MakeClaim();

            flightsController.ControllerContext = new ControllerContext();
            flightsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = flightsController.Add();
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        
        [Test]
        public void AddShouldReturnSameView()
        {
            //Arrange
            var flightsController = new FlightsController(null, null, null);
            ClaimsPrincipal user = MakeClaim();

            flightsController.ControllerContext = new ControllerContext();
            flightsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            flightsController.ModelState.AddModelError("key", "error message");


            //Act
            var result = flightsController.Add(new FlightFormModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

        }

        [Test]
        public void AddWithoutParamsShouldReturnBadReques()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();

            var flightsController = new FlightsController(FlightServiceMock.Instance.Object, null, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            flightsController.ControllerContext = new ControllerContext();
            flightsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = flightsController.Add();
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void AddWithParamsShouldReturnBadReques()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();

            var flightsController = new FlightsController(FlightServiceMock.Instance.Object, null, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            flightsController.ControllerContext = new ControllerContext();
            flightsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = flightsController.Add(new FlightFormModel
            {
                Destination = "arr",
                Origin = "arr",
                DestinationImageUrl = "arr",
                DepartureDate = DateTime.Now.ToString(),
                ReturnDate = DateTime.Now.ToString(),
                Price = 3,
            });
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }


        [Test]
        public void AddShouldRedirect()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineTrue();


            var flightsController = new FlightsController(FlightServiceMock.Instance.Object, SeatServiceMock.Instance.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            flightsController.ControllerContext = new ControllerContext();
            flightsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = flightsController.Add(new FlightFormModel
            {
                Destination = "arr",
                Origin = "arr",
                DestinationImageUrl = "arr",
                DepartureDate = DateTime.Now.ToString(),
                ReturnDate = DateTime.Now.ToString(),
                Price = 3
            });
            //Assert
            Assert.NotNull(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual(redirect.ActionName, "Index");

        }
        [Test]
        public void AllShouldReturnView()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            data.Flights.AddRange(Enumerable.Range(0, 9).Select(f => new Flight
            {
                OriginName = "Sofia",
                DestinationName = "Plovdiv",
                OriginIata = "ias",
                DestinationIata = "sar",
                DestinationImageUrl = "asdasdas"
            }));
            data.SaveChanges();
            var flightService = new FlightService(data);
            var flightsController = new FlightsController(flightService, null, null);

            //Act
            var result = flightsController.All();
            //Assert
            Assert.NotNull(result);

            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            var models = viewResult.Model as List<AllFlightsViewModel>;

            Assert.That(models[0], Is.TypeOf<AllFlightsViewModel>());
            Assert.AreEqual(9, models.Count);

        }
        [Test]
        public void SearchShouldReturnView()
        {
            //Arrange

            var data = DatabaseMock.Instance;
            data.Flights.AddRange(Enumerable.Range(0, 9).Select(f => new Flight
            {
                OriginName = "Sofia",
                DestinationName = "Plovdiv",
                OriginIata = "ias",
                DestinationIata = "sar",
                DestinationImageUrl = "asdasdas"
            }));
            data.SaveChanges();
            var flightService = new FlightService(data);
            var flightsController = new FlightsController(flightService, null, null);

            //Act
            var result = flightsController.Search("ias", null, new DateTime(0001, 01, 01), new DateTime(0001, 01, 01), null);
            //Assert
            Assert.NotNull(result);

            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as QueryModel;

            Assert.That(model, Is.TypeOf<QueryModel>());
            Assert.AreEqual(9, model.Flights.Count());

        }

    }
}