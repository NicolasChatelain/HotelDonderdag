using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        private string _name;
        private string _email;
        private string _phone;
        private string _address;
        private int _memberCount;
        private List<MemberUI> _members = new();


        public event PropertyChangedEventHandler? PropertyChanged;


        public int Id
        {
            get;
            set;
        }

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
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged();
            }

        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public int MemberCount
        {
            get { return _memberCount; }
            set
            {
                OnPropertyChanged();
            }
        }


        internal List<MemberUI> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                _memberCount = _members.Count;
                OnPropertyChanged();
            }
        }


        public CustomerUI(string name, string email, string address, string phone)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
        }

        public CustomerUI(int? id, string name, string email, string address, string phone, List<MemberUI> members)
        {
            Id = (int)id;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            Members = members;
        }

        public CustomerUI()
        {

        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal static string[] GetaddressArray(string Address)
        {

            string pattern = @"[\s-]+";
            string[] array = Regex.Split(Address, pattern);
            array[1] = array[1].Replace("[", "").Replace("]", "");

            return array;
            

        }

    }
}
