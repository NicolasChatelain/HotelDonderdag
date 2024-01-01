using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Activity = Hotel.Domain.Model.Activity;

namespace Hotel.Presentation.Mapper
{
    internal static class MapActivity
    {
        internal static ICollection<ActivityUI> FromDomainToUI(OrganizationManager om, int id, bool active, string? filter)
        {
            return om.GetAllActivities(id, active, filter)
                .Select(x => new ActivityUI(
                        x.Id,
                        x.Capacity,
                        x.Fixture,
                        x.IsUpcoming,
                        x.Description.Name,
                        x.Description.DetailedDescription,
                        x.Description.Location,
                        x.Description.Duration,
                        x.PriceInfo.AdultPrice,
                        x.PriceInfo.ChildPrice,
                        x.PriceInfo.DiscountPercentage,
                        x.PriceInfo.AdultAge
                        )).ToList();
        }

        internal static ActivityUI FromDomainToUI(Activity activity)
        {
            return new ActivityUI(
                 activity.Id,
                 activity.Capacity,
                 activity.Fixture,
                 activity.IsUpcoming,
                 activity.Description.Name,
                 activity.Description.DetailedDescription,
                 activity.Description.Location,
                 activity.Description.Duration,
                 activity.PriceInfo.AdultPrice,
                 activity.PriceInfo.ChildPrice,
                 activity.PriceInfo.DiscountPercentage,
                 activity.PriceInfo.AdultAge
            );
        }

        internal static Activity ToDomain(ActivityUI selectedActivity)
        {
            return new Activity()
            {
                Id = selectedActivity.Id,
                Capacity = selectedActivity.MaximumCapacity,
                Fixture = selectedActivity.Fixture,
                Description = new()
                {
                     DetailedDescription = selectedActivity.DetailedDescription,
                     Location = selectedActivity.Location,
                     Duration = selectedActivity.Duration,
                     Name = selectedActivity.Name,
                },
                PriceInfo = new()
                {
                    AdultAge = selectedActivity.AdultAge,
                    AdultPrice = selectedActivity.AdultPrice,
                    ChildPrice = selectedActivity.ChildPrice,
                    DiscountPercentage = selectedActivity.DiscountPercentage,
                },
            };
        }
    }
}
