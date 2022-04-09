using Microsoft.EntityFrameworkCore;
using FlightBookingSystemProject.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using FlightBookingSystemProject.Areas.Admin;

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

            SeedAdministrator(services);
            return app;
        }
        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminConstants.AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdminConstants.AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@admin";
                    const string adminPassword = "admin1";

                    var user = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }

}
