using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Customer
    {

        private const int _maxNameLength = 500;
        private int _id;
        private string _name;
        private ContactInfo _contact;

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

        private readonly List<Member> _members = new();

        private readonly List<Registration> _registrations = new();



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
                throw new CustomerException("Addmember");
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
                throw new CustomerException("remove member");
            }
        }

    }
}
