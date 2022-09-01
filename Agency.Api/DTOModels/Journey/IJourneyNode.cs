using Agency.Data.DB;
using Agency.Data.Models.Contracts;

namespace Agency.Api.DTOModels.Journey
{
    public interface IJourneyNode
    {
        string Destination { get; set; }
        int Distance { get; set; }
        string JourneyID { get; set; }
        decimal Price { get; set; }
        string StartLocation { get; set; }
        string VehicleModel { get; set; }

        Task<JourneyNode> MakeJourneyNode(IJourney journey, AgencyDBContext dBContext);
        Task<List<JourneyNode>> MakeListOfJourneyNodes(List<IJourney> journeys, AgencyDBContext dBContext);
    }
}