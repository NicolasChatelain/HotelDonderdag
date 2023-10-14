using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime Fixture {  get; set; }
        public int Capacity {  get; set; }
        public Description Description { get; set; }
        public PriceInfo PriceInfo { get; set; }

    }
}
