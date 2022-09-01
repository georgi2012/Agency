using Agency.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Models.VehicleValidators.Models
{
    public class BusValidator : IValidator
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
                catch (ArgumentException)
                {
                    //revert changes
                    _price = current;
                    throw;
                }
            }
        }

        public BusValidator(int pasCapacity, decimal price, IValidator _additValidator)
        {
            _pasCapacity = pasCapacity;
            _price = price;
            _additionalValidator = _additValidator;
        }

        public BusValidator(int pasCapacity, decimal price)
            : this(pasCapacity, price, new PhysicsValidator(pasCapacity,price))
        { }


        public bool isValid()
        {
            if (PasCapacity < 10 || PasCapacity > 50)
            {
                throw new ArgumentException("A bus cannot have less than 10 passengers or more than 50 passengers.");
            }
            return _additionalValidator != null ? _additionalValidator.isValid() : true;
        }
    }
}
