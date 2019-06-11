using System;
using WebApi.HypermediaExtensions.Hypermedia;
using WebApi.HypermediaExtensions.Hypermedia.Attributes;
using WebApi.HypermediaExtensions.WebApi.RouteResolver;

namespace RoboPlant.Server.REST.RobotBlueprint
{
    [HypermediaObject(Title = "A robot blueprint ready for production", Classes = new[] { "RobotBlueprint" })]
    public class RobotBlueprintHto : HypermediaObject
    {
        [Key]
        [FormatterIgnoreHypermediaProperty]
        public Guid Id { get; }

        public long Version { get; }

        public string Name { get; }

        public string Description { get; }
        
        public long ProductionEfford { get; }

        public RobotBlueprintHto(Domain.Design.RobotBlueprint robotBlueprint)
        {
            Id = robotBlueprint.Id.Value;
            Version = robotBlueprint.Version;
            Name = robotBlueprint.HumanReadableName;
            Description = robotBlueprint.Description;
            ProductionEfford = robotBlueprint.ProductionEfford;
        }
    }
}