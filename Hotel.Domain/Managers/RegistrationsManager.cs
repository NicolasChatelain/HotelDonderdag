using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System.Collections;

namespace Hotel.Domain.Managers
{
    public class RegistrationsManager
    {
        private readonly IRegistrationsRepository _repository;
        public RegistrationsManager(IRegistrationsRepository repository)
        {

            _repository = repository;

        }

        public List<Activity> GetAllActivities()
        {
            try
            {
                return _repository.GetAllActivities();
            }
            catch (Exception ex)
            {
                throw new RegistrationsManagerException(ex.Message);
            }
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

        public (int RegistrationID, List<Member>) GetSubscribedMembersForAcitivity(int activityId, int customerId)
        {
            try
            {
                return _repository.GetSubscribedMembersForActivity(activityId, customerId);
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

        public void MakeRegistration(List<Member> members, Activity activity)
        {
            try
            {
                bool RegistrationSucces = _repository.MakeRegistration(members, activity);
            }
            catch (Exception ex)
            {
                throw new RegistrationsManagerException(ex.Message);
            }
        }
    }
}
