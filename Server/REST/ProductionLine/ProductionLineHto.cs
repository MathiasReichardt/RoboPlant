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
            Id = productionLine.Id.Value;
            Name = productionLine.HumanReadableName;
            State = productionLine.State;
            ShutDownForMaintenance = new ShutDownForMaintenance(() => productionLine.ShutDownForMaintenance.HasValue, () => {}); // we dont need the execute
            CompleteMaintenance = new CompleteMaintenance(() => productionLine.CompleteMaintenance.HasValue, () => {});          // we dont need the execute
        }

        [Key]
        [FormatterIgnoreHypermediaProperty]
        public Guid Id { get; set; }

        public ProductionLineState State { get; set; }

        public string Name { get; set; }

        [HypermediaAction(Name = "ShutDownForMaintenance", Title = "Shuts down the production line in an orderly fashion to do maintenance.")]
        public ShutDownForMaintenance ShutDownForMaintenance { get; set; }

        [HypermediaAction(Name = "CompleteMaintenance", Title = "Completes the maintenance of the production line.")]
        public CompleteMaintenance CompleteMaintenance { get; set; }
    }

    public class ShutDownForMaintenance : HypermediaAction
    {
        public ShutDownForMaintenance(Func<bool> canExecute, Action command) : base(canExecute, command)
        {
        }
    }

    public class CompleteMaintenance : HypermediaAction
    {
        public CompleteMaintenance(Func<bool> canExecute, Action command) : base(canExecute, command)
        {
        }
    }
}
