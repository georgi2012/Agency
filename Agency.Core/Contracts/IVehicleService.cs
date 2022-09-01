using Agency.Data.Models.Contracts;
using Agency.Data.Models.Vehicles.Contracts;

namespace Agency.Core.Contracts
{
    public interface IVehicleService
    {
        Task<List<IVehicle>> GetVehiclesAsync();
        Task<IVehicle> GetVehicleWithIdAsync(int id);
        Task RemoveVehicleAsync(int id);
    }
}