using FlightBookingSystemProject.Controllers;
using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Services.Travelogues;
using FlightBookingSystemProject.Test.Mock;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBookingSystemProject.Test.Services
{
    public class TravelogueServiceTest
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
            data.Travelogues.Add(new Travelogue
            {
                Id = 1,
                Title = "Test1",
                Subtitle = "TestSubtitle",
                Destination = "Barcelona",
                Topic = "TestTopic",
                Content = "12345567890123456789012345678901234567890123456789012355678901234567990123455679901234567890",
                Email = "ad@ad",
                DestinationImageUrl = "asd",
                SecondImageUrl = "dsa",
                UserId = "1"
            });

            data.SaveChanges();
            return data;
        }

        [Test]
        public void AddShoulCreateTravelogueInData()
        {
            FlightBookingDbContext data = fillDatabase();
            var travelogueService = new TravelogueService(data);
            //Act
            travelogueService.Add("1", "test2", "testsubtitle", "contetshouldbe50charectersso12345678901234567890123456789012345678901234567890", "asd", "dsa", "TopicForTest", "Sofia/Bulgaria", "sa@sa");
            //Assert   
            Assert.AreEqual(2, data.Travelogues.Count());
        }

        [Test]
        public void AllShouldReturnAllTravelogues()
        {
            FlightBookingDbContext data = fillDatabase();
            var travelogueService = new TravelogueService(data);
            //Act
            var result = travelogueService.GetAll();
            //Assert
            Assert.NotNull(result);
            Assert.That(result[0], Is.TypeOf<Travelogue>());
            Assert.That(result, Is.TypeOf<List<Travelogue>>());
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetByIdShuldReturnTravelogue()
        {
            FlightBookingDbContext data = fillDatabase();
            var travelogueService = new TravelogueService(data);
            //Act
            var result = travelogueService.GetById(1);
            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<Travelogue>());
        }

    }
}