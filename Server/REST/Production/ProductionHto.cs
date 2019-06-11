using System.Collections.Generic;
using System.Linq;
using RoboPlant.Server.REST.ProductionLine;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.Hypermedia.Links;

namespace RoboPlant.Server.REST.Production
{
    [HypermediaObject(Title = "Access to the available production capabilities of the RoboPlant", Classes = new[] { "Production" })]
    public class ProductionHto : HypermediaObject
    {
        public ProductionHto(ICollection<Domain.Production.ProductionLine> productionLines)
        {
            var productionLineHtos = productionLines.Select(p => 
                new RelatedEntity("ProductionLine", new HypermediaObjectReference(new ProductionLineHto(p))));

            Entities.AddRange(productionLineHtos);
        }
    }
}
