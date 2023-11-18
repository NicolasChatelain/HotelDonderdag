using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        private int _capacity;
        private DateTime _fixture;
        private Description _description;
        private PriceInfo _priceInfo;

        public int Id { get; set; }
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
            }
        }
        public DateTime Fixture
        {
            get { return _fixture; }
            set
            {
                if (value.Year >= 1900)
                {
                    _fixture = value;
                }
                else
                {
                    throw new ActivityException("Minimum year must is 1900.");
                }
            }
        }
        public Description Description
        {
            get { return _description; }
            set
            {
                if (value is not null)
                {
                    _description = value;
                }
            }
        }
        public PriceInfo PriceInfo
        {
            get { return _priceInfo; }
            set
            {
                if (value is not null)
                {
                    _priceInfo = value;
                }
            }
        }
        public bool IsActive { get; set; }

        internal void SetCapacity(string value)
        {
            try
            {
                int capacity = int.Parse(value);
                if (capacity >= 2)
                {
                    Capacity = capacity;
                }
                else
                {
                    throw new ActivityException("Capacity must be minimum 2.");
                }

            }
            catch (FormatException)
            {
                throw new ActivityException("Capacity must be a valid number.");
            }
        }
        internal void SetFixture(string fixture)
        {
            string pattern = @"^\d{2}[./]\d{2}[./]\d{4} [\d: \-]{8,}$";

            if (Regex.IsMatch(fixture, pattern))
            {
                Fixture = DateTime.Parse(fixture);
            }
            else
            {
                throw new ActivityException("Enter a valid Date and Time");
            }
        }
    }
}
