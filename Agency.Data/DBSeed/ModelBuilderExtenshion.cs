using Agency.Data.DB;
using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Data.DBSeed
{
    public static class ModelBuilderExtenshion
    {
        public static void Seeder(this ModelBuilder builder)
        {
            var boat = new Boat
            {
                //VehicleID = 1,
                Type = VehicleType.Sea,
                PassangerCapacity = 90,
                PricePerKilometer = 4.1m,
                OffersWaterSports = true
            };

            var plane = new Airplane
            {
                //VehicleID = 4,
                Type = VehicleType.Air,
                PassangerCapacity = 250,
                PricePerKilometer = 2.9m,
                HasFreeFood = false

            };

            var bus = new Bus
            {
                //VehicleID = 2,
                Type = VehicleType.Land,
                PassangerCapacity = 15,
                PricePerKilometer = 2.3m
            };

            var train = new Train
            {
               // VehicleID = 3,
                Type = VehicleType.Land,
                PassangerCapacity = 100,
                PricePerKilometer = 4m,
                Carts = 6

            };

            List<Journey> journeys = new()
            {
               new Journey{
                   Destination = "London",
                   Distance= 1518,
                   StartLocation ="Lovech",
                   JourneyID = new Guid("c7bdc365-7a0b-4c04-ff70-08da874aa142"),
                   VehicleID = plane.VehicleID,
                   Vehicle= plane
               },
               new Journey{
                   Destination ="Huan Muan",
                   Distance=2891,
                   StartLocation ="Sofia",
                   JourneyID = new Guid("00000000-7a0b-4c04-ff70-08da874aa142"),
                   VehicleID = train.VehicleID,
                   Vehicle= train
               }
            };

            builder.Entity<Boat>().HasData(boat);

            builder.Entity<Bus>().HasData(bus);

            builder.Entity<Train>().HasData(train);

            builder.Entity<Airplane>().HasData(plane); ;

            builder.Entity<Journey>().HasData(journeys);

            builder.Entity<Ticket>().HasData(
                new Ticket
                {
                    AdministrativeCosts = 200,
                    Journey = journeys[0],
                    JourneyID = journeys[0].JourneyID,
                    //TicketID = new Guid("0000dc65-7a0b-4c04-ff70-08da874aa142")
                },
                new Ticket
                {
                    AdministrativeCosts = 1200,
                    Journey = journeys[1],
                    JourneyID = journeys[1].JourneyID,
                    //TicketID = new Guid("1000d365-7a0b-4c04-ff70-08da874aa142")
                });

        }
    }
}
