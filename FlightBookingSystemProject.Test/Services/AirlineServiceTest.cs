using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBookingSystemProject.Test.Services
{
    public class AirlineServiceTest
    {
        private static FlightBookingDbContext fillDatabase()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            data.Flights.AddRange(Enumerable.Range(0, 9).Select(f => new Flight
            {
                OriginIata = "ias",
                DestinationIata = "sar",
                DepartureDate = new DateTime(2022,1,1),
                ReturnDate = new DateTime(2022, 1, 2),
                Price = 1,
                DestinationImageUrl = "asdasdas"
            })) ;
            data.Airlines.Add(new Airline
            {
                Id = 1,
                Name = "1",
                UserId = "1"
            });

            data.SaveChanges();
            return data;
        }

        [Test]
        public void CreateShouldAddAirline()
        {
            //Arrange
            FlightBookingDbContext data = fillDatabase();
            var airlineService = new AirlineService(data);
            //Act
            airlineService.CreateAirline("2", "2");
            //Assert
            Assert.AreEqual(2, data.Airlines.Count());
        }

        [Test]
        public void GetAirlineNameShouldReturnName()
        {
            //Arrange
            FlightBookingDbContext data = fillDatabase();
            var airlineService = new AirlineService(data);
            //Act
            var result = airlineService.GetAirlineName(1);
            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<string>());
            Assert.AreEqual("1", result);
        }

        [Test]
        public void IsAirlineShouldReturnTrue()
        {
            //Arrange
            FlightBookingDbContext data = fillDatabase();
            var airlineService = new AirlineService(data);
            //Act
            var result = airlineService.IsAirline("1");
            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<bool>());
            Assert.AreEqual(true, result);
        } 
    }
}