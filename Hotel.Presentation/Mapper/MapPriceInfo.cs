using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Presentation.Mapper
{
    internal class MapPriceInfo
    {
        internal static List<PriceInfoUI> FromDomainToUI(List<PriceInfo> prices)
        {
            return prices.Select(x => new PriceInfoUI(x.ID, x.AdultPrice, x.ChildPrice, x.DiscountPercentage, x.AdultAge)).ToList();
        }
    }
}
