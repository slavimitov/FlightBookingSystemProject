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

                return flightServiceMock;


            }
        }
    }
}
