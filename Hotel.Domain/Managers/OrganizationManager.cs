﻿using Hotel.Domain.Interfaces;
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
    }
}
