using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContactInfo Contact { get; set; }
        List<Activity> Activities { get; }

        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
        }

        public void RemoveActivity(Activity activity)
        {
            Activities.Remove(activity);
        }
    }
}
