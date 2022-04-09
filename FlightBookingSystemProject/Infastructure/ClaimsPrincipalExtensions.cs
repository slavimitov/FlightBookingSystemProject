using FlightBookingSystemProject.Areas.Admin;
using System.Security.Claims;

namespace FlightBookingSystemProject.Infastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminConstants.AdministratorRoleName);
    }
}
