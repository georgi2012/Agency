using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Agency.Data.DB;
using Agency.Data.Models.Vehicles.Models;
using Agency.Data.Models.Vehicles.Contracts;
using Agency.Data.Models.Contracts;

namespace Agency.UnitTests
{
    public class AgencyUtils
    {
        public static DbContextOptions<AgencyDBContext> GetInMemoryOptions(string nameOfDb)
        {
            return new DbContextOptionsBuilder<AgencyDBContext>()
                .UseInMemoryDatabase(databaseName: nameOfDb)
                .Options;
        }
        public static void Seed(AgencyDBContext context)
        {
            var boat = new Boat
            {
                VehicleID = 1,
                Type = VehicleType.Sea,
                PassangerCapacity = 90,
                PricePerKilometer = 4.1m,
                OffersWaterSports = true
            };

            var bus = new Bus
            {
                VehicleID = 2,
                Type = VehicleType.Land,
                PassangerCapacity = 15,
                PricePerKilometer = 2.3m
            };

            var train = new Train
            {
                VehicleID = 3,
                Type = VehicleType.Land,
                PassangerCapacity = 100,
                PricePerKilometer = 4m,
                Carts = 6

            };
            var plane = new Airplane
            {
                VehicleID = 4,
                Type = VehicleType.Air,
                PassangerCapacity = 250,
                PricePerKilometer = 2.9m,
                HasFreeFood = false

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

            context.Boats.Add(boat);

            context.Buses.Add(bus);

            context.Trains.Add(train);

            context.Airplanes.Add(plane); ;

            context.Journeys.AddRange(journeys);

            context.Tickets.AddRange(
                new Ticket
                {
                    AdministrativeCosts = 200,
                    Journey = journeys[0],
                    JourneyID = journeys[0].JourneyID,
                    TicketID = new Guid("0000dc65-7a0b-4c04-ff70-08da874aa142")
                },
                new Ticket
                {
                    AdministrativeCosts = 1200,
                    Journey = journeys[1],
                    JourneyID = journeys[1].JourneyID,
                    TicketID = new Guid("1000d365-7a0b-4c04-ff70-08da874aa142")
                });

            context.SaveChanges();
        }

        public static AgencyDBContext InMemorySeededContextGenerator()
        {
            var context = InMemoryEmptyContextGenerator();
            Seed(context);
            return context;
        }
        public static AgencyDBContext InMemoryEmptyContextGenerator()
        {
            var context = new AgencyDBContext(GetInMemoryOptions(Guid.NewGuid().ToString()));
            context.Database.EnsureCreated();
            return context;
        }
    }
}
