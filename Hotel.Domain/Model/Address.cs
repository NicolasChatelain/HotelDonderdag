using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Address
    {
        private const char splitChar = '|';
        private string _city;
        private string _street;
        private string _postalCode;
        private string _houseNumber;


        public string City
        {
            get { return _city; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomerException("Invalid City.");
                }
                _city = value;
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomerException("Invalid Street.");
                }
                _street = value;
            }
        }

        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomerException("Invalid ZipCode.");
                }
                _postalCode = value;
            }
        }

        public string HouseNumber
        {
            get { return _houseNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomerException("Invalid Housenumber.");
                }
                _houseNumber = value;
            }
        }

        public Address(string city, string street, string postalcode, string housenumber)
        {
            City = city;
            Street = street;
            PostalCode = postalcode;
            HouseNumber = housenumber;
        }

        public Address(string addressLine)
        {
            string[] parts = addressLine.Split(splitChar);
            City = parts[0];
            PostalCode = parts[1];
            Street = parts[2];
            HouseNumber = parts[3];
        }

        public Address()
        {
        }

        public override string ToString()
        {
            return $"{City} [{PostalCode}] - {Street} - {HouseNumber}";
        }

        public string ToAddressLine()
        {
            return $"{City}{splitChar}{PostalCode}{splitChar}{Street}{splitChar}{HouseNumber}";
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Address other = (Address)obj;
            return City == other.City && Street == other.Street && PostalCode == other.PostalCode && HouseNumber == other.HouseNumber; 
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(City, Street, PostalCode, HouseNumber);
        }
    }
}
