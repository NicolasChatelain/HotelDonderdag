using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class OrganizationManager
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationManager(IOrganizationRepository repo)
        {
            _organizationRepository = repo;
        }

        public Organization ValidateInputs(string name, string email, string phone, string city, string street, string postalcode, string nr)
        {
            Organization org;
            try
            {
                Address address = new(city, street, postalcode, nr);
                ContactInfo contact = new(email, phone, address);
                org = new(name, contact);
            }
            catch
            {
                throw;
            }

            return org;

        }

        public int AddOrganization(object org)
        {
            try
            {
                return _organizationRepository.AddOrganization((Organization)org);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateOrganization(int id, object org)
        {
            try
            {
                _organizationRepository.UpdateOrganization(id, (Organization)org);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveOrganziation(int ID)
        {
            try
            {
                _organizationRepository.RemoveOrganization(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Organization> GetAllOrganizations()
        {
            try
            {
                return _organizationRepository.GetAllOrganizations();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Activity> GetAllActivities(int id, bool onlyActives, string? filter)
        {
            try
            {
                return _organizationRepository.GetAllActivitiesByOrganization(id, onlyActives, filter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveActivity(int id)
        {
            try
            {
                _organizationRepository.RemoveActivity(id);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Activity ValidateActivity(string name, string fixture, string capacity, string location, string duration, string adultprice, string kidsprice, string discount, string adultage, string description)
        {
            try
            {
                PriceInfo p = new();
                p.SetAdultPrice(adultprice);
                p.SetKidsPrice(kidsprice);
                p.SetDiscount(discount);
                p.SetAdultAge(adultage);

                Description d = new()
                {
                    Location = location,
                    DetailedDescription = description,
                    Name = name,
                };
                d.SetDuration(duration);

                Activity a = new()
                {
                    Description = d,
                    PriceInfo = p,
                };
                a.SetFixture(fixture);
                a.SetCapacity(capacity);

                return a;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddActivityToOrganization(Activity a, int orgID)
        {
            try
            {
                _organizationRepository.AddActivty(a, orgID);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Description> GetAllDescriptions(int orgID)
        {
            try
            {
                return _organizationRepository.GetAllDescriptions(orgID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PriceInfo> GetAllPrices(int orgID)
        {
            try
            {
                return _organizationRepository.GetAllPrices(orgID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidateExistingActivty(string fixture, string capacity)
        {
            try
            {
                Activity a = new();
                a.SetCapacity(capacity);
                a.SetFixture(fixture);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int PlanExistingActivity(int ID, string fixture, string capacity, int id, int orgID)
        {
            return _organizationRepository.PlanExistingActivity(ID, fixture, capacity, id, orgID);
        }
    }
}
