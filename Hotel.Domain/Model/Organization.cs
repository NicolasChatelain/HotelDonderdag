using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Organization
    {
        private string _name;


        public int Id { get; set; }
        public string Name
        {
            get { return  _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new OrganizationException("Invalid organization name");
                }
                _name = value;
            }
        }
        public ContactInfo Contact { get; set; }
        private List<Activity> Activities { get; }

        public Organization(string name, ContactInfo contact)
        {
            Name = name;
            Contact = contact;
        }

        public Organization()
        {
            
        }



        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
        }

        public void RemoveActivity(Activity activity)
        {
            Activities.Remove(activity);
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
