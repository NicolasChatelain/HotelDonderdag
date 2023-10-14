using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        private string _name;
        private string _email;
        private string _address;
        private string _phone;
        private int _nrOfMembers;

        public event PropertyChangedEventHandler? PropertyChanged;


        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value; OnPropertyChanged();
            }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }
        public int NrOfMembers
        {
            get { return _nrOfMembers; }
            set
            {
                _nrOfMembers = value; OnPropertyChanged();
            }
        }


        public CustomerUI(string name, string email, string address, string phone, int nrOfMembers)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
        }

        public CustomerUI(int? id, string name, string email, string address, string phone, int nrOfMembers)
        {
            Id = (int)id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            NrOfMembers = nrOfMembers;
        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



       
    }
}
