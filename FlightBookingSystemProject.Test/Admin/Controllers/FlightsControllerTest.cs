using FlightBookingSystemProject.Areas.Admin.Controllers;
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

namespace FlightBookingSystemProject.Test.Admin.Controllers
{
    public class FlightsControllerTest
    {

        [Test]
        public void DeleteGetShouldReturnView()
        {

            //Arrange
            var flightsController = new FlightsController(null);    
            //Act
            var result = flightsController.Delete(1);
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        
        [Test]
        public void DeletePostShouldReturnRedirect()
        {
            //Arrange
            var flightsController = new FlightsController(FlightServiceMock.Instance.Object);
            //Act
            var result = flightsController.DeleteConfirmed(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectResult>(result);
        }

        [Test]
        public void EditGetShouldReturnView()
        {
            //Arrange
            var flightsController = new FlightsController(FlightServiceMock.Instance.Object);
            //Act
            var result = flightsController.Edit(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void EditPostShouldReturnRedirect()
        {
            //Arrange
            var flightsController = new FlightsController(FlightServiceMock.Instance.Object);
            var formModel = new FlightFormModel { Origin = "asd", Destination = "dsa", ReturnDate = "1/2/2022", DepartureDate = "1/2/2022", Price = 1, DestinationImageUrl = "asd" };
            //Act
            var result = flightsController.Edit(1, formModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}