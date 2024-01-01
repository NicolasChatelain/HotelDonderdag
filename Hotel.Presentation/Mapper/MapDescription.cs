using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System.Collections.Generic;
using System.Linq;


namespace Hotel.Presentation.Mapper
{
    internal class MapDescription
    {
        internal static List<DescriptionUI> FromDomainToUI(List<Description> descriptions)
        {
            return descriptions.Select(x => new DescriptionUI(x.ID, x.Name, x.DetailedDescription, x.Duration.ToString(), x.Location)).ToList();
        }
    }
}
