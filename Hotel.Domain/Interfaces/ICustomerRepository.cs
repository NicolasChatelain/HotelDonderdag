using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IReadOnlyList<Customer> GetCustomers(string filter);

        int AddCustomer(Customer customer);
        void RemoveCustomer(int customerID); // sets user inactive, stays in DB
        void UpdateCustomer(Customer customer);
        
        void AddMembers(int id, List<Member> member);
        void RemoveMember(int id, Member member);
        void UpdateMember(int id, Member memberOriginalState, Member memberUpdatedState);
    }
}
