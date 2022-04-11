using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FlightBookingSystemProject.Test.Controllers
{
    public class HomeControllerTest
    {

        [Test]
        public void PrivacyShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null);
            //Act
            var result = homeController.Privacy();
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            data.Flights.AddRange(Enumerable.Range(0, 9).Select(f => new Flight
            {
                OriginIata = "ias",
                DestinationIata = "sar",
                DestinationImageUrl = "asdasdas"
            }));
            data.SaveChanges();
            var flightService = new FlightService(data);
            var homeController = new HomeController(null, flightService);
            //Act
            var result = homeController.Index();
            //Assert
            Assert.NotNull(result);
            
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            var models = viewResult.Model as List<AllFlightsViewModel>;
            
            Assert.That(models[0], Is.TypeOf<AllFlightsViewModel>());
            Assert.AreEqual(8, models.Count);

        }

        //[Test]
        //public void ErrorShouldReturnView()
        //{
        //    //Arrange
        //    var homeController = new HomeController(null, null);
        //    //Act
        //    var result = homeController.Error();

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.IsInstanceOf<ViewResult>(result);
        //}
    }
}