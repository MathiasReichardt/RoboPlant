using System.Collections.Generic;
using System.Linq;
using Bluehands.Hypermedia.Relations;
using RoboPlant.Server.REST.RobotBlueprint;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.Hypermedia.Links;

namespace RoboPlant.Server.REST.Design
{
    [HypermediaObject(Title = "Robot blueprint query result", Classes = new[] { "BlueprintQueryResult" })]
    public class BlueprintQueryResultHto : HypermediaQueryResult
    {
        public long Count { get; set; }

        public BlueprintQueryResultHto(ICollection<Domain.Design.RobotBlueprint> entities, BlueprintQueryParameters queryParameters) : base(queryParameters)
        {
            Count = entities.Count;

            var relatedBlueprints = entities.Select(b => new RelatedEntity(DefaultHypermediaRelations.EmbeddedEntities.Item, new HypermediaObjectReference(new RobotBlueprintHto(b))));
            Entities.AddRange(relatedBlueprints);
        }
    }
}