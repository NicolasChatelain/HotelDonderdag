using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Model
{
    public class MemberUI
    {
        private string _name;
        private string _birthday;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public MemberUI(string name, string birthday)
        {
            Name = name;
            Birthday = birthday;
        }

       
    }
}
