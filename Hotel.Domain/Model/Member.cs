﻿using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Member
    {

        private string _name;
        private DateOnly _birthday;

        public int ID { get; set; }
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
                _name = value.ToLower();
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
                if (DateOnly.FromDateTime(DateTime.Now) <= value || value <= new DateOnly(1753, 1, 1))
                {
                    throw new CustomerException("Invalid date.");
                }
                _birthday = value;
            }
        }
        public Customer Customer { get; set; }

        public Member(string name, DateOnly birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public Member(int id, string name, DateOnly birthday) : this(name, birthday)
        {
            ID = id;
        }

        public Member(int id, string name, DateOnly birthday, Customer customer) : this(id, name, birthday)
        {
            Customer = customer;
        }


        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }


            Member otherMember = (Member)obj;
            return ID == otherMember.ID && Name == otherMember.Name && Birthday == otherMember.Birthday;

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Birthday);
        }
    }
}