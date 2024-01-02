using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class ContactInfo
    {
        private const char ValidEmailCheck = '@';
        private const byte ValidCharCount = 1;

        private string _email;
        private string _phone;
        private Address _address;

        public ContactInfo(string email, string phone, Address address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Count(@char => @char == ValidEmailCheck) != ValidCharCount)
                {
                    throw new ContactInfoException("This email is not valid, Email must contain 1 @ character.");
                }
                _email = value;
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Any(char.IsDigit))
                {
                    throw new ContactInfoException("This is not a valid phonenumber.");
                }

                string parsedPhoneNumber = Regex.Replace(value, @"\D", "");
                _phone = parsedPhoneNumber.Trim(); ;
            }
        }

        public Address Address
        {
            get { return _address; }
            set
            {
                if (value is null)
                {
                    throw new ContactInfoException("Not a valid adrress.");
                }
                _address = value;
            }
        }



    }
}
