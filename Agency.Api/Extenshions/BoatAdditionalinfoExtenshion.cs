using Agency.Data.Models.Vehicles;
using Agency.Data.Models.Vehicles.Models;

namespace Agency.Api.Extenshions
{
    public static class BoatAdditionalinfoExtenshion
    {
        public static string AdditionalInfo(this Boat boat)
        {
            string ans = boat.OffersWaterSports ? "Yes" : "No";
            return $"Offers water sports: {ans}";
        }
    }
}
