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
                throw new CustomerManagerException("GetCustomers");
            }
        }

        public int AddCustomer(Customer customer)
        {
             return _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }

        
    }
}
