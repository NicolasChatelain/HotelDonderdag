using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    class MemberUI
    {
        private string _name;
        private DateOnly _birthday;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateOnly Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
    }
}
