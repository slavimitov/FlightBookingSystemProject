using Microsoft.EntityFrameworkCore;
using FlightBookingSystemProject.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace FlightBookingSystemProject.Infastructure
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<FlightBookingDbContext>();

            db.Database.Migrate();

            return app;
        }

    }
   

}
