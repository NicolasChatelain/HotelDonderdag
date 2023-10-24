using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    class OrganizationUI
    {
        public OrganizationUI(string name, string email, string phone, string city, string street, string postalcode, string housenumber)
        {
            Name = name;
            Email = email;
            Phone = phone;
            City = city;
            Street = street;
            Postalcode = postalcode;
            Housenumber = housenumber;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City {  get; set; }
        public string Street {  get; set; }
        public string Postalcode { get; set; }
        public string Housenumber {  get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
