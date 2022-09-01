using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.Models.VehicleValidators.Contacts
{
    public interface ITrainValidator : IValidator
    {
        int Carts { get; set; }
    }
}
