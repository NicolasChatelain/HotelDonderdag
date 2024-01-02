using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Customer
    {

        private const int _maxNameLength = 500;
        private int _id;
        private string _name;
        private ContactInfo _contact;
        private List<Member> _members = new();

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > _maxNameLength)
                {
                    throw new CustomerException($"The {GetType().Name} can not be empty and can not contain more than {_maxNameLength} characters.");
                }
                _name = value;
            }
        }

        public ContactInfo Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        public Customer(int id, string name, ContactInfo ci)
        {
            Id = id;
            Name = name;
            Contact = ci;
        }

        public Customer(string name, ContactInfo ci)
        {
            Name = name;
            Contact = ci;
        }

        public Customer(int id)
        {
            Id = id;
        }


        public IReadOnlyList<Member> GetMembers()
        {
            return _members.AsReadOnly();
        }

        public void AddMember(Member member)
        {
            if (!_members.Contains(member))
            {
                _members.Add(member);
            }
            else
            {
                throw new CustomerException("Add member");
            }
        }

        public void DeleteMember(Member member)
        {
            if (_members.Contains(member))
            {
                _members.Remove(member);
            }
            else
            {
                throw new CustomerException("Remove member");
            }
        }

        internal Customer AppendAllMembers(List<Member> members)
        {
            _members = members;
            return this;
        }


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Customer other = (Customer)obj;
            return Id == other.Id && Name == other.Name && Contact.Equals(other.Contact);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Contact.GetHashCode());
        }

    }
}
