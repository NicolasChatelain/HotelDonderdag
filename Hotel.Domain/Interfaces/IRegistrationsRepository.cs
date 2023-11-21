using Hotel.Domain.Model;

namespace Hotel.Domain.Interfaces
{
    public interface IRegistrationsRepository
    {
        List<Member> GetMembersForCustomer(int customerId);
        Dictionary<int, string> GetValidLoginPhones();
    }
}
