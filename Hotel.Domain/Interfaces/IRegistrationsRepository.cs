using Hotel.Domain.Model;

namespace Hotel.Domain.Interfaces
{
    public interface IRegistrationsRepository
    {
        List<Member> GetMembersForCustomer(int customerId);
        Dictionary<int, (string, string)> GetValidLoginPhones();
        List<Activity> GetAllActivities();
        (int RegistrationID, List<Member>) GetSubscribedMembersForActivity(int activityId, int customerId);
        bool MakeRegistration(List<Member> members, Activity activity);
    }
}
