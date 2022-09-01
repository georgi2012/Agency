using Agency.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Models.VehicleValidators.Models
{
    public class PhysicsValidator : IValidator
    {

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

        public PhysicsValidator(int pasCapacity, decimal price)
        {
            this._pasCapacity = pasCapacity;
            this._price = price;
        }

        public bool isValid()
        {
            if (_pasCapacity < 1 || _pasCapacity > 800)
            {
                throw new ArgumentException("A vehicle with less than 1 passengers or more than 800 passengers cannot exist!");
            }
            if (_price < 0.1m || _price > 2.5m)
            {
                throw new ArgumentException("A vehicle with a price per kilometer lower than $0.10 or higher than $2.50 cannot exist!");
            }
            return true;
        }

    }
}
