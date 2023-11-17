using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        private int _capacity;
        private DateTime _fixture;

        public int Id { get; set; }
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value >= 2)
                {
                    _capacity = value;
                }
                else
                {
                    throw new ActivityException("Capacity must be minimum 2");
                }
            }
        }
        public DateTime Fixture
        {
            get { return _fixture; }
            set
            {
                if (_fixture is not null && _fixture.Year > 1900)
                {
                    _fixture = value;
                }
                else
                {
                    throw new ActivityException("Fixture was invalid.");
                }
            }
        }
        public Description Description { get; set; }
        public PriceInfo PriceInfo { get; set; }
        public bool IsActive { get; set; }

    }
}
