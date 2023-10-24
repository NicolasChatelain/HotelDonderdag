using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private string connectionstring;

        public OrganizationRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }



    }
}
