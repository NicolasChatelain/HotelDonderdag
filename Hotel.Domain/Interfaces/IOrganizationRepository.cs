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
        void UpdateOrganization(int id, Organization org);
        void RemoveOrganization(int ID);

        public List<Organization> GetAllOrganizations();
        List<Activity> GetAllActivitiesByOrganization(int id, bool onlyActives, string? filter);
        void RemoveActivity(int id);
        void AddActivty(Activity a,int orgID);
        List<Description> GetAllDescriptions(int orgID);
        List<PriceInfo> GetAllPrices(int orgID);
        int PlanExistingActivity(int iD, string fixture, string capacity, int id, int orgID);
    }
}
