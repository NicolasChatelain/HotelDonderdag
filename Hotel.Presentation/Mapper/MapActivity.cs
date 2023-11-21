﻿using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal static ActivityUI FromUItoUI(Activity activity)
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
    }
}
