using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBookingSystemProject.Data.Migrations
{
    public partial class DateChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TakeOffTime",
                table: "Flights",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Flights",
                newName: "DepartureDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Flights",
                newName: "TakeOffTime");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Flights",
                newName: "DepartureTime");
        }
    }
}
