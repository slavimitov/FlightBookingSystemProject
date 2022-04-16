using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightBookingSystemProject.Test.Controllers
{
    public class SeatsControllerTest
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
                                        new Claim(ClaimTypes.Email, "ad@com")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            return user;
        }


        [Test]
        public void BookShouldReturnView()
        {

            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();
            var seatService = SeatServiceMock.Instance;
            var seatsController = new SeatsController(seatService.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            seatsController.ControllerContext = new ControllerContext();
            seatsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = seatsController.Book(1);
            //Assert
            Assert.NotNull(result);

            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            var models = viewResult.Model as List<SelectListItem>;
           Assert.That(models[0], Is.TypeOf<SelectListItem>());

        }

        [Test]
        public void BookShouldRedirectToAllWithoutForm()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineTrue();
            var seatService = SeatServiceMock.Instance;
            var seatsController = new SeatsController(seatService.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            seatsController.ControllerContext = new ControllerContext();
            seatsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = seatsController.Book(1);
            //Assert
            Assert.NotNull(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual(redirect.ActionName, "All");

        }

        [Test]
        public void BookShouldRedirectToIndex()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();
            var seatService = SeatServiceMock.Instance;
            var seatsController = new SeatsController(seatService.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            seatsController.ControllerContext = new ControllerContext();
            seatsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = seatsController.Book(null, 1);
            //Assert
            Assert.NotNull(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual(redirect.ActionName, "Index");

        }

        [Test]
        public void BookShouldRedirectToAllWithForm()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineTrue();
            var seatService = SeatServiceMock.Instance;
            var seatsController = new SeatsController(seatService.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            seatsController.ControllerContext = new ControllerContext();
            seatsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = seatsController.Book(null, 1);
            //Assert
            Assert.NotNull(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual(redirect.ActionName, "All");

        }

        [Test]
        public void MyBookedSeatsShouldRedirectToAll()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineTrue();
            var seatService = SeatServiceMock.Instance;
            var seatsController = new SeatsController(seatService.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            seatsController.ControllerContext = new ControllerContext();
            seatsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = seatsController.MyBookedSeats();
            //Assert
            Assert.NotNull(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual(redirect.ActionName, "All");
        }

        [Test]
        public void MyBookedSeatsShouldReturnView()
        {
                   
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();
            var seatService = SeatServiceMock.Instance;
            var seatsController = new SeatsController(seatService.Object, airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            seatsController.ControllerContext = new ControllerContext();
            seatsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = seatsController.MyBookedSeats();
            //Assert
            Assert.NotNull(result);

            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            var models = viewResult.Model as List<TicketViewModel>;

            Assert.That(models[0], Is.TypeOf<TicketViewModel>());
        }
    }
}