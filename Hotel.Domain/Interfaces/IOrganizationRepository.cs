using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        int AddOrganization(Organization org);
        void UpdateOrganization(Organization org);
        void RemoveOrganization(int ID);

        public List<Organization> GetAllOrganizations();
    }
}
