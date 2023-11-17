using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Description
    {
        private int _duration;
        private string _location;
        private string _detailedDescription;
        private string _name;

        public int Duration
        {
            get { return _duration; }
            set
            {
               _duration = value;
            }
        }
        public string Location
        {
            get { return _location; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _location = value;
                }
                else
                {
                    throw new DescriptionException("Location cannot be null or empty.");
                }
            }
        }
        public string DetailedDescription
        {
            get { return _detailedDescription; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _detailedDescription = value;
                }
                else
                {
                    throw new DescriptionException("Description cannot be null or empty");
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                else
                {
                    throw new DescriptionException("Name cannot be null or empty.");
                }
            }
        }

        public void SetDuration(string value)
        {
            try
            {
                int duration = int.Parse(value);
                if (duration >= 15)
                {
                    Duration = duration;
                }
                else
                {
                    throw new DescriptionException("Duration must be minimum 15 minutes.");
                }

            }
            catch (FormatException)
            {
                throw new DescriptionException("Duration must be a valid number.");
            }
        }





    }
}
