using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBookingSystemProject.Test.Services
{
    public class FlightServiceTest
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
            data.Airports.Add(new Airport
            {
                Id = 1,
                City = "london",
                IataCode = "ASA"
            });
            data.Airports.Add(new Airport
            {
                Id = 2,
                City = "Sofia",
                IataCode = "ASD"
            });

            data.SaveChanges();
            return data;
        }

        [Test]
        public void AddShouldReturnIdWithCorrectData()
        {
            FlightBookingDbContext data = fillDatabase();
            var flightService = new FlightService(data);
            //Act
            var result = flightService.Add("asa", "asd", "3/4/2022", "3/5/2022", 1, "asd", "1");
            //Assert
            Assert.NotNull(result);

            Assert.That(result, Is.TypeOf<int>());

            Assert.AreEqual(10, result);
            Assert.AreEqual(10, data.Flights.Count());
        }

        [Test]
        public void DeleteShouldRemoveFlightFromDatabase()
        {
            FlightBookingDbContext data = fillDatabase();
            var flightService = new FlightService(data);
            //Act
            flightService.DeleteFlight(1);
            //Assert
            Assert.AreEqual(8, data.Flights.Count());
        }

        [Test]
        public void EditShouldReturnTrueAndChangeFlight()
        {
            FlightBookingDbContext data = fillDatabase();
            var flightService = new FlightService(data);
            //Act
            var result = flightService.Edit(1, "asa", "asd", "3/4/2022", "3/5/2022", 1, "asd");
            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<bool>());
            Assert.AreEqual(result, true);
            Assert.AreEqual(data.Flights.First().OriginIata, "asa");
            Assert.AreEqual(9, data.Flights.Count());
        }

        [Test]
        public void GetFlightDetailsForEditShouldReturnFlight()
        {
            FlightBookingDbContext data = fillDatabase();
            var flightService = new FlightService(data);
            //Act
            var result = flightService.GetFlightDetailsForEdit(1);
            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<Flight>());
            Assert.AreEqual(result.OriginIata, "ias");
            Assert.AreEqual(9, data.Flights.Count());
        }

        [Test]
        public void GetFilteredShouldReturnAll()
        {
            FlightBookingDbContext data = fillDatabase();
            var flightService = new FlightService(data);
            //Act
            var result = flightService.GetFiltered("ias", "sar", new DateTime(2022, 1, 1), new DateTime(2022, 1, 2), "0");
            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Flight>(result[0]);
            Assert.AreEqual(9, result.Count());
        } 
    }
}