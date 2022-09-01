using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agency.Data.Migrations
{
    public partial class relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "JourneyID",
                keyValue: new Guid("00000000-7a0b-4c04-ff70-08da874aa142"));

            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "JourneyID",
                keyValue: new Guid("c7bdc365-7a0b-4c04-ff70-08da874aa142"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketID",
                keyValue: new Guid("0000dc65-7a0b-4c04-ff70-08da874aa142"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketID",
                keyValue: new Guid("1000d365-7a0b-4c04-ff70-08da874aa142"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleID",
                keyValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_JourneyID",
                table: "Tickets",
                column: "JourneyID");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_VehicleID",
                table: "Journeys",
                column: "VehicleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Vehicles_VehicleID",
                table: "Journeys",
                column: "VehicleID",
                principalTable: "Vehicles",
                principalColumn: "VehicleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Journeys_JourneyID",
                table: "Tickets",
                column: "JourneyID",
                principalTable: "Journeys",
                principalColumn: "JourneyID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Vehicles_VehicleID",
                table: "Journeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Journeys_JourneyID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_JourneyID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_VehicleID",
                table: "Journeys");

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
    }
}
