using Agency.Data.Models.Vehicles.Models;

namespace Agency.Api.Extenshions
{
    public static class BusAdditionalInfoExtenshion
    {
        public static string AdditionalInfo(this Bus bus)
        {
            return " - ";
        }
    }
}
