using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agency.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    JourneyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    VehicleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.JourneyID);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdministrativeCosts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JourneyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PassangerCapacity = table.Column<int>(type: "int", nullable: false),
                    PricePerKilometer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasFreeFood = table.Column<bool>(type: "bit", nullable: true),
                    OffersWaterSports = table.Column<bool>(type: "bit", nullable: true),
                    Carts = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleID);
                });

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "JourneyID", "Destination", "Distance", "StartLocation", "VehicleID" },
                values: new object[,]
                {
                    { new Guid("00000000-7a0b-4c04-ff70-08da874aa142"), "Huan Muan", 2891, "Sofia", 3 },
                    { new Guid("c7bdc365-7a0b-4c04-ff70-08da874aa142"), "London", 1518, "Lovech", 4 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketID", "AdministrativeCosts", "JourneyID" },
                values: new object[,]
                {
                    { new Guid("0000dc65-7a0b-4c04-ff70-08da874aa142"), 200m, new Guid("c7bdc365-7a0b-4c04-ff70-08da874aa142") },
                    { new Guid("1000d365-7a0b-4c04-ff70-08da874aa142"), 1200m, new Guid("00000000-7a0b-4c04-ff70-08da874aa142") }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleID", "Discriminator", "HasFreeFood", "PassangerCapacity", "PricePerKilometer", "Type" },
                values: new object[] { 4, "Airplane", false, 250, 2.9m, 1 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleID", "Discriminator", "OffersWaterSports", "PassangerCapacity", "PricePerKilometer", "Type" },
                values: new object[] { 1, "Boat", true, 90, 4.1m, 2 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleID", "Discriminator", "PassangerCapacity", "PricePerKilometer", "Type" },
                values: new object[] { 2, "Bus", 15, 2.3m, 0 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleID", "Carts", "Discriminator", "PassangerCapacity", "PricePerKilometer", "Type" },
                values: new object[] { 3, 6, "Train", 100, 4m, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
