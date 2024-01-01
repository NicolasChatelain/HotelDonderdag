using Hotel.Domain.Exceptions;
using System.Text.RegularExpressions;


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
                _fixture = value;
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
        public bool IsUpcoming { get; set; }

        public void SetCapacity(string value)
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
        public void SetFixture(string fixture)
        {
            string pattern = "\\d{2}/\\d{2}/\\d{4} \\d{2}:\\d{2}";


            if (!Regex.IsMatch(fixture, pattern))
            {
                throw new ActivityException("Enter a valid Date and Time");
            }
            else
            {
                DateTime datetime = DateTime.Parse(fixture);

                DateTime now = DateTime.Now;
                DateTime tomorrow = now.AddDays(1);

                if (Id > 0)
                {
                    Fixture = datetime;
                }
                else
                {
                    if (datetime > tomorrow)
                    {
                        Fixture = datetime;
                    }
                    else
                    {
                        throw new ActivityException("An activity must be planned minimum 1 day in advance.");
                    }
                }
            }
        }



    }
}
