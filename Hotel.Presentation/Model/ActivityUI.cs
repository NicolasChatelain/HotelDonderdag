using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {
        public ActivityUI(int id, int maximumCapacity, DateTime fixture, bool upcoming, string name, string detailedDescription, string location, int duration, int adultPrice, int childPrice, int discountPercentage, int adultage)
        {
            Id = id;
            MaximumCapacity = maximumCapacity;
            Fixture = fixture;
            IsUpcoming = upcoming;
            Name = name;
            DetailedDescription = detailedDescription;
            Location = location;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            DiscountPercentage = discountPercentage;
            AdultAge = adultage;
            Duration = duration;
        }
        public ActivityUI()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id;
        private int _maximumCapacity;
        private DateTime _fixture;
        private bool _isActive;
        private string _name;
        private string _detailedDescription;
        private string _location;
        private int _duration;
        private int _adultPrice;
        private int _childPrice;
        private int _discountPercentage;
        private int _adultAge;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int MaximumCapacity
        {
            get { return _maximumCapacity; }
            set
            {
                _maximumCapacity = value;
                OnPropertyChanged(nameof(MaximumCapacity));
            }
        }
        public DateTime Fixture
        {
            get { return _fixture; }
            set
            {
                _fixture = value;
                OnPropertyChanged(nameof(Fixture));
            }
        }
        public bool IsUpcoming
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsUpcoming));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string DetailedDescription
        {
            get { return _detailedDescription; }
            set
            {
                _detailedDescription = value;
                OnPropertyChanged(nameof(DetailedDescription));
            }
        }
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        public int AdultPrice
        {
            get { return _adultPrice; }
            set
            {
                _adultPrice = value;
                OnPropertyChanged(nameof(AdultPrice));
            }
        }
        public int ChildPrice
        {
            get { return _childPrice; }
            set
            {
                _childPrice = value;
                OnPropertyChanged(nameof(ChildPrice));
            }
        }
        public int DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                _discountPercentage = value;
                OnPropertyChanged(nameof(DiscountPercentage));
            }
        }
        public int AdultAge
        {
            get { return _adultAge; }
            set
            {
                _adultAge = value;
                OnPropertyChanged(nameof(AdultAge));
            }
        }

        public string ShowDetails()
        {
            return $"\nName: {Name}" +
           $"\n\nDetails: {DetailedDescription}" +
           $"\n\nLocation: {Location}" +
           $"\nDuration: {Duration}" +
           $"\n\nMaximum Capacity: {MaximumCapacity} people" +
           $"\nAdult Price: €{AdultPrice}" +
           $"\nChild Price: €{ChildPrice}" +
           $"\nDiscount Percentage: {DiscountPercentage}%" +
           $"\n\nAdult Age: {AdultAge} years" +
           $"\n\n\t\tFixture Date: {Fixture:dd-MM-yyyy HH:mm}";
        }

        public override string ToString()
        {
            return Name;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
