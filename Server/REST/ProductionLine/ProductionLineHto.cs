using System;
using RoboPlant.Domain.Production;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.WebApi.RouteResolver;

namespace RoboPlant.Server.REST.ProductionLine
{
    [HypermediaObject(Title = "Production line to assemble robots", Classes = new[] { "ProductionLine" })]
    public class ProductionLineHto : HypermediaObject
    {
        public ProductionLineHto(Domain.Production.ProductionLine productionLine)
        {
            this.Id = productionLine.Id.Value;
            this.Name = productionLine.HumanReadableName;
            this.State = productionLine.State;
        }

        [Key]
        [FormatterIgnoreHypermediaProperty]
        public Guid Id { get; set; }

        public ProductionLineState State { get; set; }

        public string Name { get; set; }
    }
}
