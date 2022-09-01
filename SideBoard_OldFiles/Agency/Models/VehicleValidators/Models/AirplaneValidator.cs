using Agency.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Models.VehicleValidators.Models
{
    public class AirplaneValidator : IValidator
    {
        private readonly IValidator _additionalValidator;
        private int _pasCapacity;
        private decimal _price;

        public int PasCapacity
        {
            get => _pasCapacity;
            set
            {
                int current = _pasCapacity;
                _pasCapacity = value;
                try
                {
                    if (_additionalValidator != null)
                    {
                        _additionalValidator.PasCapacity = value;
                    }
                    isValid();
                }
                catch (ArgumentException)
                {
                    //revert changes
                    _pasCapacity = current;
                    throw;
                }
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                decimal current = _price;
                _price = value;
                try
                {
                    if (_additionalValidator != null)
                    {
                        _additionalValidator.Price = value;
                    }
                    isValid();
                }
                catch (Exception)
                {
                    //revert changes
                    _price = current;
                    throw;
                }
            }
        }
        public AirplaneValidator(int pasCapacity, decimal price, IValidator additValidator)
        {
            _pasCapacity = pasCapacity;
            _price = price;
            _additionalValidator = additValidator;
        }

        public AirplaneValidator(int pasCapacity, decimal price)
            : this(pasCapacity, price, new PhysicsValidator(pasCapacity, price))
        { }

        public bool isValid()
        {
            return _additionalValidator != null ? _additionalValidator.isValid() : true;
        }
    }
}
