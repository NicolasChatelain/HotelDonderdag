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

        public List<Member> GetSubscribedMembersForAcitivity(int activityId, int customerId)
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

        public bool MakeRegistration(List<Member> members, Activity activity, int currentCustomerId)
        {
            try
            {
                Registration? registration = _repository.GetRegistration(activity); //haal de registratie op als ze bestaat.

                if (registration is null)
                {
                    registration = new Registration
                    {
                        Activity = activity
                    };

                    registration.Subscribe(members); // throws exception als capacity te vol geraakt
                    return _repository.MakeRegistration(registration);
                }
                else
                {
                    registration.UpdateSubscribers(members, currentCustomerId); // throws exception als capacity te vol geraakt
                    return _repository.UpdateRegistration(registration);
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationsManagerException(ex.Message);
            }
        }
    }
}
