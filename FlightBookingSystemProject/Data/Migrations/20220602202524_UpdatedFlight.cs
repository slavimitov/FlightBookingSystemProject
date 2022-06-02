using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBookingSystemProject.Data.Migrations
{
    public partial class UpdatedFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginName",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginName",
                table: "Flights");
        }
    }
}
