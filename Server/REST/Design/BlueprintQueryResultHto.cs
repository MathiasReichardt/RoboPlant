using System.Collections.Generic;
using System.Linq;
using Bluehands.Hypermedia.Relations;
using RoboPlant.Server.REST.RobotBlueprint;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.Hypermedia.Extensions;
using WebApi.HypermediaExtensions.Hypermedia.Links;

namespace RoboPlant.Server.REST.Design
{
    [HypermediaObject(Title = "Robot blueprint query result", Classes = new[] { "BlueprintQueryResult" })]
    public class BlueprintQueryResultHto : HypermediaQueryResult
    {

        public long Count { get; set; }

        public BlueprintQueryResultHto(ICollection<HypermediaObjectReferenceBase> entities, int totalBlueprintsCountEnties, BlueprintQueryParameters queryParameters)
            : base(queryParameters)
        {
            Count = entities.Count;
            Entities.AddRange(DefaultHypermediaRelations.EmbeddedEntities.Item, entities);
        }

        public BlueprintQueryResultHto(Application.Persistence.Results.QueryResult<Domain.Design.RobotBlueprint>.Success queryResult, BlueprintQueryParameters queryParameters) : base(queryParameters)
        {
            Count = queryResult.Result.Count;

            var relatedBlueprints = queryResult.Result.Select(b => new RelatedEntity(DefaultHypermediaRelations.EmbeddedEntities.Item, new HypermediaObjectReference(new RobotBlueprintHto(b))));
            this.Entities.AddRange(relatedBlueprints);
        }
    }
}