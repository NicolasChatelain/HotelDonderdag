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
    }
}
