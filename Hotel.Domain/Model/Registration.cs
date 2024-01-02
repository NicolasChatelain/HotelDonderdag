using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Registration
    {
        public int Id { get; set; }
        public Activity Activity { get; set; }
        public HashSet<Member> Subscribers { get; private set; }

        public void Subscribe(List<Member> newmembers)
        {
            if (Subscribers is null)
            {
                Subscribers = new HashSet<Member>();
            }

            AddMembersToSubscribers(newmembers);
        }

        public void UpdateSubscribers(List<Member> newmembers, int customerId)
        {
            IEnumerable<Member> AllMembersForThisCustomer = Subscribers.Where(member => member.Customer.Id == customerId);
            IEnumerable<Member> Unsubscribers = AllMembersForThisCustomer.Except(newmembers);

            foreach (Member member in Unsubscribers)
            {
                Subscribers.Remove(member);
            }

            AddMembersToSubscribers(newmembers);
        }

        private void CheckCapacity(List<Member> newmembers)
        {
            int remainingSpots = Activity.Capacity - Subscribers.Count;

            IEnumerable<Member> uniqueMembers = newmembers.Except(Subscribers);

            if (Subscribers.Count + uniqueMembers.Count() > Activity.Capacity)
            {
                throw new RegistrationException($"This activity has a maximum capacity of {Activity.Capacity}. Only {remainingSpots} spots are left.");
            }
        }

        private void AddMembersToSubscribers(List<Member> newmembers)
        {
            CheckCapacity(newmembers);
            Subscribers.UnionWith(newmembers);
        }

    }
}
