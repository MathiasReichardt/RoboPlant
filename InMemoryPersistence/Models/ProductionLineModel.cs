using System;
using RoboPlant.Domain.Production;
using SharpRepository.Repository;

namespace RoboPlant.InMemoryPersistence.Models
{
    internal class ProductionLineModel
    {
        [RepositoryPrimaryKey]
        public Guid Id { get; set; }

        public string HumanReadableName { get; set; }

        public ProductionLineState State { get; set; }

        public ProductionLineModel() {}

    }
}