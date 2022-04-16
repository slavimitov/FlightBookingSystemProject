using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Services.Flights;
using FlightBookingSystemProject.Services.Seats;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystemProject.Test.Mock
{
    public static class FlightServiceMock
    {
        public static Mock<IFlightService> Instance
        {
            get
            {
                var flightServiceMock = new Mock<IFlightService>();

                flightServiceMock
                    .Setup(x => x.Add("asd","asd","asd","asd",3,"asd", "asd"))
                    .Returns(1);

                flightServiceMock
                    .Setup(x => x.GetAll())
                    .Returns(Enumerable.Range(0, 5).Select(s => new Flight
                    {
                        DestinationIata="asd",
                        OriginIata="asd",
                        DestinationImageUrl="asd"
                    }).ToList());

                flightServiceMock
                    .Setup(x => x.GetFlightDetailsForEdit(1))
                    .Returns(new Flight
                    {
                        DestinationIata = "asd",
                        OriginIata = "asd",
                        ReturnDate = DateTime.Now,
                        DepartureDate = DateTime.Now,
                        Price = 1,
                        DestinationImageUrl = "asd"
                    });

                flightServiceMock
                    .Setup(x => x.Edit(1, "asd", "dsa", "1/2/2022", "1/2/2022", 1, "asd"))
                    .Returns(true);

                return flightServiceMock;
            }
        }
    }
}
