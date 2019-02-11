using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;

namespace RoboPlant.Server.REST.ProductionLine
{
    [HypermediaObject(Title = "Production line to assemble robots", Classes = new[] { "ProductionLine" })]
    public class ProductionLineHto : HypermediaObject
    {
    }
}
