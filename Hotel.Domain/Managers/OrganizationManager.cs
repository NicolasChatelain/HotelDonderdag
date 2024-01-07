using Hotel.Domain.Exceptions;
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
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }

            return org;

        }

        public int AddOrganization(object org)
        {
            try
            {
                return _organizationRepository.AddOrganization((Organization)org);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public void UpdateOrganization(int id, object org)
        {
            try
            {
                _organizationRepository.UpdateOrganization(id, (Organization)org);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public void RemoveOrganziation(int ID)
        {
            try
            {
                _organizationRepository.RemoveOrganization(ID);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public List<Organization> GetAllOrganizations()
        {
            try
            {
                return _organizationRepository.GetAllOrganizations();
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public List<Activity> GetAllActivities(int id, bool onlyActives, string? filter)
        {
            try
            {
                return _organizationRepository.GetAllActivitiesByOrganization(id, onlyActives, filter);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public void RemoveActivity(int id)
        {
            try
            {
                _organizationRepository.RemoveActivity(id);

            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public Activity ValidateActivity(string name, string fixture, string capacity, string location, string duration, string adultprice, string kidsprice, string discount, string adultage, string description, bool upcoming)
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
                a.IsUpcoming = upcoming;

                return a;

            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public void AddActivityToOrganization(Activity a, int orgID)
        {
            try
            {
                _organizationRepository.AddActivty(a, orgID);

            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public List<Description> GetAllDescriptions(int orgID)
        {
            try
            {
                return _organizationRepository.GetAllDescriptions(orgID);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public List<PriceInfo> GetAllPrices(int orgID)
        {
            try
            {
                return _organizationRepository.GetAllPrices(orgID);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
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
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public int PlanExistingActivity(int ID, DateTime fixture, string capacity, int id, int orgID)
        {
            try
            {
                return _organizationRepository.PlanExistingActivity(ID, fixture, capacity, id, orgID);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public bool ApplyDiscount(double discount, int id)
        {
            try
            {
                return _organizationRepository.ApplyDiscount(discount, id);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }

        public bool UpdatedFixture(DateTime updatedFixture, int id)
        {
            try
            {
                return _organizationRepository.UpdateFixture(updatedFixture, id);
            }
            catch (Exception ex)
            {
                throw new OrganizationManagerException(ex.Message);
            }
        }
    }
}
