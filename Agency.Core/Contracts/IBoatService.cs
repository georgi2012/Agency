using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Contracts
{
    public interface IBoatService
    {
        Task CreateBoatAsync(int capacity, decimal price, bool hasWaterSports);
    }
}
