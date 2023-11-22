using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;

namespace Hotel.Domain.Managers
{
    public class RegistrationsManager
    {
        private readonly IRegistrationsRepository _repository;
        public RegistrationsManager(IRegistrationsRepository repository)
        {

            _repository = repository;

        }

        public List<Member> GetMembersForCustomer(int customerId)
        {
            try
            {
                return _repository.GetMembersForCustomer(customerId);
            }
            catch (Exception ex)
            {
                throw new RegistrationsManagerException(ex.Message);
            }
        }

        public Dictionary<int, (string, string)> GetValidLoginPhones()
        {
            try
            {
                return _repository.GetValidLoginPhones();
            }
            catch (Exception ex)
            {
                throw new RegistrationsManagerException(ex.Message);
            }
        }
    }
}
