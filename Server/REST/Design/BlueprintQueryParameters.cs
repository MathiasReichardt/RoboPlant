using WebApi.HypermediaExtensions.Hypermedia.Actions;
using WebApi.HypermediaExtensions.Query;

namespace RoboPlant.Server.REST.Design
{
    public class BlueprintQueryParameters : IHypermediaActionParameter, IHypermediaQuery
    {
        public int? MaxProductionEfford { get; set; }

        public string ModelName { get; set; }
    }
}