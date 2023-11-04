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
        public ActivityUI(int id, int maximumCapacity, DateTime fixture, string name, string detailedDescription, string location, int duration, int adultPrice, int childPrice, int discountPercentage, int adultage)
        {
            Id = id;
            MaximumCapacity = maximumCapacity;
            Fixture = fixture;
            Name = name;
            DetailedDescription = detailedDescription;
            Location = location;
            Duration = duration;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            DiscountPercentage = discountPercentage;
            Adultage = adultage;
        }

        public ActivityUI(int id, DateTime fixture, string name, string location, int duration, int adultage)
        {
            Id = id;
            Fixture = fixture;
            Name = name;
            Location = location;
            Duration = duration;
            Adultage = adultage;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id { get; set; }
        public int MaximumCapacity { get; set; }
        public DateTime Fixture {  get; set; }
        public string Name { get; set; }
        public string DetailedDescription { get; set; }
        public string Location { get; set; }
        public int Duration {  get; set; }
        public int AdultPrice { get; set; }
        public int ChildPrice { get; set; }
        public int DiscountPercentage {  get; set; }
        public int Adultage { get; set; }


        public override string ToString()
        {
            return $"Name: {Name}" +
                   $"\n\n" +
                   $"Details: {DetailedDescription}" +
                   $"\n\n" +
                   $"Location: {Location}" +
                   $"\n\n" +
                   $"Price: €{AdultPrice}\n" +
                   $"Kids: €{ChildPrice}";
        }

    }
}
