using System;
using SharpRepository.Repository;

namespace RoboPlant.InMemoryPersistence.Models
{
    internal class RobotBlueprintModel
    {
        [RepositoryPrimaryKey]
        public Guid Id { get; set; }

        public long Version { get; set; }

        public string HumanReadableName { get; set; }

        public string Description { get; set; }

        public long ProductionEfford { get; set; }
    }
}