using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystemProject.Data
{
    public class FlightBookingDbContext : IdentityDbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public FlightBookingDbContext(DbContextOptions<FlightBookingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
              .Entity<IdentityUserLogin<string>>()
              .HasNoKey();

            base.OnModelCreating(builder);
        }
    }
}