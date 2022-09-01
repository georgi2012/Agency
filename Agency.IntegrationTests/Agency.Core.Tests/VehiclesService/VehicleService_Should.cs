using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agency.Data.DB;
using Microsoft.EntityFrameworkCore;
using Agency.Core.Services.TicketServices;
using Agency.Data.Models.Vehicles.Models;
using Agency.Core.Services.JourneyServices;
using Agency.UnitTests;
using Agency.Data.Models.Contracts;
using Agency.Core.Services.VehicleServices;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.IntegrationTests.VehiclesService
{
    public class VehicleService_Should
    {

        public async static void AddAirplane(AgencyDBContext context)
        {
            int capacity = DataRestrictions.MaxAirplaneCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            AirplaneService airplaneService = new AirplaneService(context);
            await airplaneService.CreateAirplaneAsync(capacity, price, true);
        }

        public async static void AddBus(AgencyDBContext context)
        {
            int capacity = DataRestrictions.MaxBusCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            BusService busService = new BusService(context);
            await busService.CreateBusAsync(capacity, price);
        }

        public async static void AddTrain(AgencyDBContext context)
        {
            int capacity = DataRestrictions.MinTrainCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            int carts = DataRestrictions.MaxTrainCarts;
            TrainService trainService = new TrainService(context);
            await trainService.CreateTrainAsync(capacity, price, carts);
        }

        public async static void AddBoat(AgencyDBContext context)
        {
            int capacity = DataRestrictions.MaxAirplaneCapacity;
            decimal price = DataRestrictions.MaxPricePerKm;
            BoatService boatService = new BoatService(context);
            await boatService.CreateBoatAsync(capacity, price, true);
        }

        public static IEnumerable<object[]> AddVehicleToDB()
        {
            decimal price = DataRestrictions.MaxPricePerKm;
            Action<AgencyDBContext>[] creators =
                { (db) => AddAirplane(db) ,
                   db=>AddBus(db),
                   db=> AddTrain(db),
                   db=>AddBoat(db)
                 };
            int[] capacities =
                {
                    DataRestrictions.MaxAirplaneCapacity,
                    DataRestrictions.MaxBusCapacity,
                    DataRestrictions.MinTrainCapacity,
                    DataRestrictions.MaxAirplaneCapacity
                };
            for (int i = 0; i < creators.Length; i++)
            {
                yield return new object[] {  creators[i], capacities[i], price };
            }

        }


        [Theory]
        [MemberData(nameof(AddVehicleToDB))]
        public async void VehicleService_ShouldRemoveJourneysThatUseTheVehicleAfterItIsRemoved
            (Action<AgencyDBContext> AddVehicleTo, int capacity, decimal price)
        {
            //journeys data
            const int distance1 = 350;
            const int distance2 = 200;
            string location1 = "abudabi";
            string location2 = "los angelis";
            string destination1 = "dolno poduene";
            string destination2 = "sen trope";
            //
            AgencyDBContext inmDbContext = AgencyUtils.InMemorySeededContextGenerator();
            TicketService ticketService = new(inmDbContext);
            JourneyService journeyService = new JourneyService(inmDbContext, ticketService);
            VehicleService vehService = new VehicleService(inmDbContext, journeyService);
            //make some vehicle
            AddVehicleTo(inmDbContext);
            //find the added vehicle for the ID
            IVehicle veh = (await vehService.GetVehiclesAsync()).
                FindLast(x => x.PassangerCapacity == capacity && x.PricePerKilometer == price);
            var journeysBeforeAdding = await journeyService.GetJourneyAsync();
            //make journeys with it
            await journeyService.CreateJourneyAsync(location1, destination1, distance1, veh);
            await journeyService.CreateJourneyAsync(location2, destination2, distance2, veh);
            //making sure that both journeys are added
            Assert.Equal(journeysBeforeAdding.Count + 2, (await journeyService.GetJourneyAsync()).Count);
            //now remove the vehicle
            await vehService.RemoveVehicleAsync(veh.VehicleID);
            //and check if journeys are also removed
            var journeysAfterRemoving = await journeyService.GetJourneyAsync();
            Assert.Equal(journeysBeforeAdding.Count, journeysAfterRemoving.Count);
            var ticketsThatUsedTheJourney = journeysAfterRemoving
                .FindAll(x => x.VehicleID == veh.VehicleID);
            Assert.True(ticketsThatUsedTheJourney.Count == 0);

        }
    }
}
