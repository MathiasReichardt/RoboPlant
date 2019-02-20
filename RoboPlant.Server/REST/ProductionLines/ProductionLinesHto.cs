using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboPlant.Server.REST.ProductionLine;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.Hypermedia.Links;

namespace RoboPlant.Server.REST.ProductionLines
{
    [HypermediaObject(Title = "Access to the available production lines of the RoboPlant", Classes = new[] { "ProductionLines" })]
    public class ProductionLinesHto : HypermediaObject
    {
        public ProductionLinesHto(ICollection<Domain.Production.ProductionLine> productionLines)
        {
            var productionLineHtos = productionLines.Select(p => 
                new RelatedEntity("ProductionLine", new HypermediaObjectReference(new ProductionLineHto(p))));

            this.Entities.AddRange(productionLineHtos);
        }
    }
}
