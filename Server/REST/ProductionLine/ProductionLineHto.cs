using System;
using RoboPlant.Domain.Production;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Actions;
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
            this.ShutDown = new ShutDown(() => productionLine.ShutDownForMaintenance.HasValue, () => {}); // we dont need the execute
        }

        [Key]
        [FormatterIgnoreHypermediaProperty]
        public Guid Id { get; set; }

        public ProductionLineState State { get; set; }

        public string Name { get; set; }

        [HypermediaAction(Name = "ShutDownForMaintenance", Title = "Shuts down the production line in an orderly fashion.")]
        public ShutDown ShutDown { get; set; }
    }

    public class ShutDown : HypermediaAction
    {
        public ShutDown(Func<bool> canExecute, Action command) : base(canExecute, command)
        {
        }
    }
}
