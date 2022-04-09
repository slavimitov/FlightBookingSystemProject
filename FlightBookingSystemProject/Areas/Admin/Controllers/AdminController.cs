using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FlightBookingSystemProject.Areas.Admin.AdminConstants;

namespace FlightBookingSystemProject.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
