using Agency.Data.Models.Vehicles.Models;

namespace Agency.Api.Extenshions
{
    public static class TrainAdditionalInfoExtenshion
    {
        public static string AdditionalInfo(this Train train)
        {
            return $"Carts count: {train.Carts}";
        }
    }
}
