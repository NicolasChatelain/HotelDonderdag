using Hotel.Domain.Model;

namespace Hotel.Domain.Interfaces
{
    public interface IRegistrationsRepository
    {
        List<Member> GetMembersForCustomer(int customerId);
        Dictionary<int, (string, string)> GetValidLoginPhones();
        List<Activity> GetAllActivities();
        List<Member> GetSubscribedMembersForActivity(int activityId, int customerId);
        bool MakeRegistration(Registration registration);
        Registration? GetRegistration(Activity activity);
        bool UpdateRegistration(Registration registration);
    }
}
