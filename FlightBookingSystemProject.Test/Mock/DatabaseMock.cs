using FlightBookingSystemProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystemProject.Test.Mock
{
    public static class DatabaseMock
    {
        public static FlightBookingDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FlightBookingDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new FlightBookingDbContext(dbContextOptions);   
            }
        }
    }
}
