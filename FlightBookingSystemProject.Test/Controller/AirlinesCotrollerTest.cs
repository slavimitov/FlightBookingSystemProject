using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightBookingSystemProject.Test.Controllers
{
    public class AirlinesControllerTest
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
        public void BecomeShouldReturnView()
        {

            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();

            var airlinesController = new AirlinesController(airlineServiceMock.Object);
            //Act
            var result = airlinesController.Become();
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void BecomeShouldReturnBadReques()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineTrue();

            var airlinesController = new AirlinesController(airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            airlinesController.ControllerContext = new ControllerContext();
            airlinesController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            //Act
            var result = airlinesController.Become(new BecomeAirlineFormModel
            {
                Name = "slav"
            });
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void BecomeShouldCreateAirlineAndRedirect()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();

            var airlinesController = new AirlinesController(airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            airlinesController.ControllerContext = new ControllerContext();
            airlinesController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var result = airlinesController.Become(new BecomeAirlineFormModel
            {
                Name = "slav" 
            });

            //Assert
            Assert.NotNull(result);
            var redirect = (RedirectToActionResult)result;
            Assert.AreEqual(redirect.ActionName, "Index");

        }


        [Test]
        public void BecomeShouldReturnSameView()
        {
            //Arrange

            Mock<IAirlineService> airlineServiceMock = SetupIsAirlineFalse();

            var airlinesController = new AirlinesController(airlineServiceMock.Object);
            ClaimsPrincipal user = MakeClaim();

            airlinesController.ControllerContext = new ControllerContext();
            airlinesController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            airlinesController.ModelState.AddModelError("key", "error message");


            var data = DatabaseMock.Instance;


            //Act
            var result = airlinesController.Become(new BecomeAirlineFormModel
            {
                Name = "slav"
            });

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

        }    
    }
}