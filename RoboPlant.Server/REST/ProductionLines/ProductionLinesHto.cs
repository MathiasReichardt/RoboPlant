using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;

namespace RoboPlant.Server.REST.ProductionLines
{
    [HypermediaObject(Title = "Access to the available production lines of the RoboPlant", Classes = new[] { "ProductionLines" })]
    public class ProductionLinesHto : HypermediaObject
    {
    }
}
