using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Member
    {

        private string _name;
        private DateOnly _birthday;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomerException("Invalid name.");
                }
                _name = value;
            }
        }
        public DateOnly Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if (DateOnly.FromDateTime(DateTime.Now) <= value)
                {
                    throw new CustomerException("Invalid date.");
                }
                _birthday = value;
            }
        }


        public Member(string name, DateOnly birthday)
        {

            Name = name;
            Birthday = birthday;

        }



        public override bool Equals(object? obj)
        {
            return obj is Member member && _name == member._name && _birthday.Equals(member._birthday);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _birthday);
        }
    }
}