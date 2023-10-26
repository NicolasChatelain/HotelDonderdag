using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    internal class OrganizationUI : INotifyPropertyChanged
    {
        private string _name;
        private string _email;
        private string _phone;
        private string _city;
        private string _street;
        private string _postalcode;
        private string _housenumber;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int ID { get; set; }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(); } }
        public string City { get { return _city; } set { _city = value; OnPropertyChanged(); } }
        public string Street { get { return _street; } set { _street = value; OnPropertyChanged(); } }
        public string Postalcode { get { return _postalcode; } set { _postalcode = value; OnPropertyChanged(); } }
        public string Housenumber { get { return _housenumber; } set { _housenumber = value; OnPropertyChanged(); } }

        public OrganizationUI(int id, string name, string email, string phone, string city, string street, string postalcode, string housenumber)
        {
            ID = id;
            Name = name;
            Email = email;
            Phone = phone;
            City = city;
            Street = street;
            Postalcode = postalcode;
            Housenumber = housenumber;
        }

        public override string ToString()
        {
            return Name;
        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
