using Agency.Data.DB;
using Agency.Data.Models.Contracts;

namespace Agency.Api.DTOModels.Journey
{
    public class JourneyNode : IJourneyNode
    {

        public JourneyNode()
        {

        }
        public JourneyNode(string jId, string destin, string startLoc,
            int dist, string vehModel, decimal price)
        {
            JourneyID = jId;
            Destination = destin;
            StartLocation = startLoc;
            Distance = dist;
            VehicleModel = vehModel;
            Price = price;
        }
        public string JourneyID { get; set; }
        public string Destination { get; set; }
        public string StartLocation { get; set; }
        public int Distance { get; set; }
        public string VehicleModel { get; set; }
        public decimal Price { get; set; }

        public virtual async Task<JourneyNode> MakeJourneyNode(IJourney journey, AgencyDBContext dBContext)
        {
            var veh = dBContext.Vehicles.FirstOrDefault(el => el.VehicleID == journey.VehicleID);
            if (veh == null)
            {
                throw new Exception("Journey's vehicle was not found in the database");
            }

            return new JourneyNode(journey.JourneyID.ToString(), journey.Destination, journey.StartLocation,
                journey.Distance, veh.ToString(), journey.CalculateTravelCosts());
        }
        public virtual async Task<List<JourneyNode>> MakeListOfJourneyNodes(List<IJourney> journeys, AgencyDBContext dBContext)
        {
            List<JourneyNode> journeyNodes = new List<JourneyNode>();
            foreach (var journey in journeys)
            {
                journeyNodes.Add(await MakeJourneyNode(journey, dBContext));
            }
            return journeyNodes;
        }
    }
}
