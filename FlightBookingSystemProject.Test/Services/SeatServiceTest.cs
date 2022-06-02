using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Airlines;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Services.Seats;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBookingSystemProject.Test.Services
{
    public class SeatServiceTest
    {
        private static FlightBookingDbContext fillDatabase()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            data.Flights.AddRange(Enumerable.Range(0, 9).Select(f => new Flight
            {
                OriginName = "Sofia",
                DestinationName = "Plovdiv",
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
            data.Seats.Add(new Seat
            {
                Id = 1,
                Initials = "1A",
                IsBooked = false,
                FlightId = 1
            });
            data.Seats.Add(new Seat
            {
                Id = 2,
                Initials = "1B",
                IsBooked = true,
                FlightId = 1
            });
            data.Tickets.Add(new Ticket
            {
                Id = 1,
                UserId = "1",
                FlightId = 1,
                SeatId = 2
            });

            data.SaveChanges();
            return data;
        }

        [Test]
        public void BookShouldThrow()
        {
            //Arrange
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "2", "2" }
            });
            FlightBookingDbContext data = fillDatabase();
            var seatService = new SeatService(data);
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => seatService.BookSeats(formCollection, 1, "1"));

        }

        [Test]
        public void BookShouldNotThrow()
        {
            //Arrange
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "1", "1" }
            });
            FlightBookingDbContext data = fillDatabase();
            var seatService = new SeatService(data);
            //Act
            //Assert
            Assert.DoesNotThrow(() => seatService.BookSeats(formCollection, 1, "1"));
            Assert.AreEqual(2, data.Tickets.Count());
        }

        [Test]
        public void CreateSetasShouldAddSeats()
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
            var seatService = new SeatService(data);
            //Act
            seatService.CreateSeats(1);
            //Assert
            Assert.AreEqual(62, data.Seats.Count());
        }

        [Test]
        public void GetSeatsShouldReturnSeats()
        {
            //Arrange
            FlightBookingDbContext data = fillDatabase();
            var seatService = new SeatService(data);
            //Act
            var result = seatService.GetSeats(1);
            //Assert
            Assert.NotNull(result);
            Assert.That(result[0], Is.TypeOf<Seat>());
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetBookedSeatsShouldReturnCorrect()
        {
            //Arrange
            FlightBookingDbContext data = fillDatabase();
            var seatService = new SeatService(data);
            //Act
            var result = seatService.GetBookedSeats("1");
            //Assert
            Assert.NotNull(result);
            Assert.That(result[0], Is.TypeOf<Ticket>());
            Assert.AreEqual(1, result.Count());
        }
    }
}