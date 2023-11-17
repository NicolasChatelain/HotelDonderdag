using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class PriceInfo
    {
        private int _adultPrice;
        private int _childPrice;
        private int _discountPercentage;
        private int _adultAge;

        public int AdultPrice
        {
            get { return _adultPrice; }
            set
            {
                if (value >= 0)
                {
                    _adultPrice = value;
                }
                else
                {
                    throw new PriceInfoException("AdultPrice must be non-negative.");
                }
            }
        }
        public int ChildPrice
        {
            get { return _childPrice; }
            set
            {
                if (value >= 0)
                {
                    _childPrice = value;
                }
                else
                {
                    throw new PriceInfoException("ChildPrice must be non-negative.");
                }
            }
        }
        public int DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _discountPercentage = value;
                }
                else
                {
                    throw new PriceInfoException("DiscountPercentage must be between 0 and 100.");
                }
            }
        }
        public int AdultAge
        {
            get { return _adultAge; }
            set
            {
                if (value >= 0)
                {
                    _adultAge = value;
                }
                else
                {
                    throw new PriceInfoException("AdultAge must be non-negative.");
                }
            }
        }

    }
}
