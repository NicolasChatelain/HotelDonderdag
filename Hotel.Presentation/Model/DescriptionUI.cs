using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel.Presentation.Model
{
    public class DescriptionUI
    {
        public DescriptionUI(int id, string name, string description, string duration, string location)
        {
            ID = id;
            Name = name;
            Description = description;
            Duration = duration;
            Location = location;
        }

        public int ID { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
