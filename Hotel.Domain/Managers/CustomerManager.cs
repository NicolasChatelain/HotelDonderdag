using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepo)
        {
            _customerRepository = customerRepo;
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch (Exception)
            {
                throw new CustomerManagerException("Something went wrong when retrieving the customers.");
            }
        }

        public int AddCustomer(Customer customer, List<Member> members)
        {
            try
            {
                MemberAppending(customer, members);
                return _customerRepository.AddCustomer(customer);
            }
            catch (Exception)
            {
                throw new CustomerManagerException(ExceptionMessage("when adding this customer and his member(s), task aborted."));
            }
        }

        public void UpdateCustomerOnly(Customer customer)
        {
            try
            {
                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception)
            {
                throw new CustomerManagerException(ExceptionMessage("updating this customer, update aborted."));
            }
        }

        public void RemoveCustomer(int customerID) // sets customer inactive, stays in DB
        {
            try
            {
                _customerRepository.RemoveCustomer(customerID);
            }
            catch (Exception)
            {
                throw new CustomerManagerException(ExceptionMessage("deleting this customer, delete aborted."));
            }
        }

        public void AddNewMembers(int id, List<Member> oldmembers, List<Member> members)
        {
            try
            {
                List<Member> newMembers = members.Except(oldmembers).ToList();
                _customerRepository.AddMembers(id, newMembers);
            }
            catch (Exception)
            {
                throw new CustomerManagerException(ExceptionMessage("adding the member(s), task aborted."));
            }
        }

        public void RemoveMember(int id, Member member)
        {
            try
            {
                _customerRepository.RemoveMember(id, member);
            }
            catch (Exception)
            {
                throw new CustomerManagerException(ExceptionMessage("deleting this member, delete aborted."));
            }
        }

        public void UpdateMember(int id, Member memberOriginalState, Member memberUpdatedState)
        {
            try
            {
                _customerRepository.UpdateMember(id, memberOriginalState, memberUpdatedState);
            }
            catch (Exception)
            {
                throw new CustomerManagerException(ExceptionMessage("updating this member, update aborted."));
            }
        }


        private static string ExceptionMessage(string specializedMessage)
        {
            return $"Something went wrong when {specializedMessage}";
        }
        private static Customer MemberAppending(Customer customer, List<Member> members)
        {
            return customer.AppendAllMembers(members);
        }
    }
}
