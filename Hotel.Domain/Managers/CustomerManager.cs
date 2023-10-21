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
            MemberAppending(customer, members);
            return _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomerOnly(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }

        public void RemoveCustomer(int customerID) // sets customer inactive, stays in DB
        {
            _customerRepository.RemoveCustomer(customerID);
        }

        public void AddNewMembers(int id, List<Member> oldmembers, List<Member> members)
        {
            List<Member> newMembers = members.Except(oldmembers).ToList();
            _customerRepository.AddMember(id, newMembers);
        }

        public void RemoveMember(int id, Member member)
        {
            _customerRepository.RemoveMember(id, member);
        }

        public void UpdateMember(int id, Member memberOriginalState, Member memberUpdatedState)
        {
            _customerRepository.UpdateMember(id, memberOriginalState, memberUpdatedState);
        }

        

        private static Customer MemberAppending(Customer customer, List<Member> members)
        {
            return customer.AppendAllMembers(members);
        }
    }
}
