namespace RoboPlant.Domain.Design
{
    public class RobotBlueprint
    {
        public RobotBlueprintId Id { get; }

        public long Version { get; }

        public string HumanReadableName { get; }

        public string Description { get; }

        public long ProductionEfford { get; }

        public RobotBlueprint(RobotBlueprintId id, long version, string humanReadableName, string description, long productionEfford)
        {
            Id = id;
            Version = version;
            HumanReadableName = humanReadableName;
            Description = description;
            ProductionEfford = productionEfford;
        }
    }
}
