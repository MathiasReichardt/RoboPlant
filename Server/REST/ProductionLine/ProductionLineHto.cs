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
            ShutDownForMaintenance = new ShutDownForMaintenance(() => productionLine.ShutDownForMaintenance.HasValue, () => {});                      // we dont need the execute
            CompleteMaintenance = new CompleteMaintenance(() => productionLine.CompleteMaintenance.HasValue, () => {});                               // we dont need the execute
            ProduceRobot = new ProduceRobot(() => productionLine.ProduceRobot.HasValue, parameters => throw new NotImplementedException("Not used")); // we dont need the execute
        }

        [Key]
        [FormatterIgnoreHypermediaProperty]
        public Guid Id { get; set; }

        public ProductionLineState State { get; set; }

        public string Name { get; set; }

        [HypermediaAction(Name = "ShutDownForMaintenance", Title = "Shut down the production line in an orderly fashion to do maintenance.")]
        public ShutDownForMaintenance ShutDownForMaintenance { get; set; }

        [HypermediaAction(Name = "CompleteMaintenance", Title = "Complete the maintenance of the production line.")]
        public CompleteMaintenance CompleteMaintenance { get; set; }

        [HypermediaAction(Name = "ProduceRobot", Title = "Queue a robot blueprint for production.")]
        public ProduceRobot ProduceRobot { get; set; }
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

    public class ProduceRobot : HypermediaFunction<ProductionRequestParameters, ProductionStateResult>
    {
        public ProduceRobot(Func<bool> canExecute, Func<ProductionRequestParameters, ProductionStateResult> command) : base(canExecute, command)
        {
        }
    }

    public class ProductionStateResult
    {
        // TODO
        // result state for hto production
        // id
    }

    public class ProductionRequestParameters : IHypermediaActionParameter
    {
        // TODO
        // id of blueprint
    }
}
