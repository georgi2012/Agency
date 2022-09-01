using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Contracts
{
    public interface IAirplaneService
    {
        Task CreateAirplaneAsync(int capacity, decimal price, bool hasFreeFood);
    }
}
