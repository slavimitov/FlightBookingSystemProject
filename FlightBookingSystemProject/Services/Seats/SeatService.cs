using FlightBookingSystemProject.Data;
using FlightBookingSystemProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlightBookingSystemProject.Services.Seats
{
    public class SeatService : ISeatService
    {
        private readonly FlightBookingDbContext data;
        public SeatService(FlightBookingDbContext data)
        {
            this.data = data;
        }

        public void BookSeats(IFormCollection formCollection, int flightId, string userId)
        {
            var seats = data.Seats.Where(s => s.FlightId == flightId).ToList();
            var firstSeat = seats.First();
            var lastSeat = seats.Last();
            for (int i = firstSeat.Id; i <= lastSeat.Id; i++)
            {
                if (!string.IsNullOrEmpty(formCollection[$"{i}"]))
                {
                    var seat = seats.FirstOrDefault(s => s.Id == int.Parse(formCollection[$"{i}"]));
                    if (seat.IsBooked)
                    {
                        throw new ArgumentException("This seat is already booked");
                    }
                    
                    seat.IsBooked = true;
                    data.Tickets.Add(new Ticket
                    {
                        UserId = userId,
                        SeatId = seat.Id,
                        FlightId = seat.FlightId

                    });
                    data.SaveChanges();
                }
            }
        }

        public void CreateSeats(int flightId)
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 65; j <= 70; j++)
                {
                    this.data.Seats.Add(new Seat
                    {
                        Initials = i.ToString() + ((char)j).ToString(),
                        IsBooked = false,
                        FlightId = flightId
                    });
                }
            }
            data.SaveChanges();
        }

        public List<Seat> GetSeats(int flightId)
        {
            return data.Seats.Where(s => s.FlightId == flightId).ToList();
        }

        public List<Ticket> GetBookedSeats(string userId)
        { 
            return data.Tickets.Where(t => t.UserId == userId).Include(t => t.Flight).Include(t => t.Seat).ToList();
        }
    }
}
