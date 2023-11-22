using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Registration
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<Member> Members { get; set; }
    }
}
