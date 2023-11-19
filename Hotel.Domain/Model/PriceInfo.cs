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

        public int ID {  get; set; }
        public int AdultPrice
        {
            get { return _adultPrice; }
            set
            {
                _adultPrice = value;
            }
        }
        public int ChildPrice
        {
            get { return _childPrice; }
            set
            {
                _childPrice = value;
            }
        }
        public int DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                _discountPercentage = value;
            }
        }
        public int AdultAge
        {
            get { return _adultAge; }
            set
            {
                _adultAge = value;
            }
        }


        public void SetAdultPrice(string value)
        {
            try
            {
                int price = int.Parse(value);
                if (price >= 0)
                {
                    AdultPrice = price;
                }
                else
                {
                    throw new PriceInfoException("Adult price must be non-negative.");
                }

            }
            catch (FormatException)
            {
                throw new PriceInfoException("Adult price must be a number.");
            }
        }

        public void SetKidsPrice(string value)
        {
            try
            {
                int price = int.Parse(value);
                if (price >= 0)
                {
                    ChildPrice = price;
                }
                else
                {
                    throw new PriceInfoException("Kids price must be non-negative.");
                }

            }
            catch (FormatException)
            {
                throw new PriceInfoException("Kids price must be a valid number.");
            }
        }

        public void SetDiscount(string value)
        {
            try
            {
                int discount = int.Parse(value);
                if (discount >= 0 && discount <= 100)
                {
                    DiscountPercentage = discount;
                }
                else
                {
                    throw new PriceInfoException("Discount must be between 0 and 100.");
                }

            }
            catch (FormatException)
            {
                throw new PriceInfoException("Discount must be a number.");
            }
        }

        public void SetAdultAge(string value)
        {
            try
            {
                int age = int.Parse(value);
                if (age >= 10)
                {
                    AdultAge = age;
                }
                else
                {
                    throw new PriceInfoException("Age must be minimum 10.");
                }

            }
            catch (FormatException)
            {
                throw new PriceInfoException("Age must be a number.");
            }
        }

    }
}
