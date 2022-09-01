using Agency.Data.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.Models.VehicleValidators.Models
{
    public class BoatValidator : IValidator
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

        public BoatValidator(int pasCapacity, decimal price, IValidator additValidator)
        {
            this._additionalValidator = additValidator;
            this._pasCapacity = pasCapacity;
            this._price = price;
        }

        public BoatValidator(int pasCapacity, decimal price)
            : this(pasCapacity, price, new PhysicsValidator(pasCapacity, price))
        { }


        public bool isValid()
        {
            if (PasCapacity < 1 || PasCapacity > 250)
            {
                throw new ArgumentException("A boar cannot have less than 1 passengers or more than 250 passengers.");
            }
            return _additionalValidator != null ? _additionalValidator.isValid() : true;
        }
    }
}
