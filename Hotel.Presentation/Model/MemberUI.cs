using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    public class MemberUI : INotifyPropertyChanged
    {
        private string _name;
        private string _birthday;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();

            }
        }

        public string Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public MemberUI(string name, string birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            MemberUI m = (MemberUI)obj;
            return Name == m.Name && Birthday == m.Birthday;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Birthday);
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
