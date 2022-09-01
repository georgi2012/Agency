using Agency.Data.Models.VehicleValidators.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Data.Models.VehicleValidators.Models
{
    public class TrainValidator : ITrainValidator
    {
        private int _carts;
        private readonly IValidator _additionalValidator;
        private int _pasCapacity;
        private decimal _price;

        public int Carts
        {
            get => _carts;
            set
            {
                int current = _carts;
                _carts = value;
                try
                {
                    isValid();
                }
                catch (ArgumentException)
                {
                    //revert changes
                    _carts = current;
                    throw;
                }
            }
        }
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
                catch (Exception)
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
        public TrainValidator(int pasCapacity, decimal price, int carts, IValidator additValidator)
        {
            this._additionalValidator = additValidator;
            this._carts = carts;
            this._pasCapacity = pasCapacity;
            this._price = price;
        }

        public TrainValidator(int pasCapacity, decimal price, int carts)
            : this(pasCapacity, price, carts, new PhysicsValidator(pasCapacity, price))
        { }

        public bool isValid()
        {
            if (PasCapacity < 30 || PasCapacity > 150)
            {
                throw new ArgumentException("A train cannot have less than 30 passengers or more than 150 passengers.");
            }
            if (Carts < 1 || Carts > 15)
            {
                throw new ArgumentException("A train cannot have less than 1 cart or more than 15 carts.");
            }
            return _additionalValidator != null ? _additionalValidator.isValid() : true;
        }
    }
}
