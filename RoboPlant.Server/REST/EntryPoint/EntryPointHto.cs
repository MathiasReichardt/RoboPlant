using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;

namespace RoboPlant.Server.REST.EntryPoint
{
    [HypermediaObject(Title = "Entry to the RoboPlant REST API", Classes = new[] { "EntryPoint" })]
    public class EntryPointHto : HypermediaObject
    {
    }
}
