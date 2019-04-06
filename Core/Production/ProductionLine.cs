using System;
using RoboPlant.Domain.Ids;

namespace RoboPlant.Domain.Production
{
    public class ProductionLine
    {
        public ProductionLineId Id { get; }

        public string HumanReadableName { get; }

        public ProductionLineState State { get; }

        public ProductionLine(ProductionLineId id, string humanReadableName, ProductionLineState state)
        {
            Id = id;
            HumanReadableName = humanReadableName;
            State = state;
        }
    }

    public class ProductionLineId : IdBase
    {
        public ProductionLineId(Guid value) : base(value)
        {
        }
    }
}

    