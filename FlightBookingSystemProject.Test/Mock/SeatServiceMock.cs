using FlightBookingSystemProject.Data;
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
    public static class SeatServiceMock
    {
        public static Mock<ISeatService> Instance
        {
            get
            {
                var seatServiceMock = new Mock<ISeatService>();

                seatServiceMock
                    .Setup(x => x.GetSeats(1))
                    .Returns(Enumerable.Range(0, 60).Select(s => new Seat
                    {
                        Initials = "1A",
                        IsBooked = false,
                    }).ToList());

                seatServiceMock
                   .Setup(x => x.CreateSeats(0));

                seatServiceMock
                    .Setup(x => x.GetBookedSeats("1", "da@com"))
                    .Returns(Enumerable.Range(0, 10).Select(s => new Ticket
                    {
                        SeatId = 1,
                        Flight = new Flight { OriginIata ="123", DestinationIata = "123", DestinationImageUrl = "as"},
                        Seat = new Seat { Initials = "1a", IsBooked = true}
                    }).ToList());
                return seatServiceMock;


            }
        }
    }
}
