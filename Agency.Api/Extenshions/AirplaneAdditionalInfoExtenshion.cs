using Agency.Data.Models.Vehicles.Models;

namespace Agency.Api.Extenshions
{
    public static class AirplaneAdditionalInfoExtenshion
    {
        public static string AdditionalInfo(this Airplane airplane)
        {
            string ans = airplane.HasFreeFood == true ? "Yes" : "No";
            return $"Offers free food: {ans}";
        }
    }
}
