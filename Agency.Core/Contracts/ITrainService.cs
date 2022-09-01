using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Contracts
{
    public interface ITrainService
    {
        Task CreateTrainAsync(int capacity, decimal price, int carts);
    }
}
