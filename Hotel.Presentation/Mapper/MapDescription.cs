using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
