using System;
using Optional;

namespace RoboPlant.Domain.Production
{
    public class ProductionLine
    {
        public ProductionLineId Id { get; }

        public string HumanReadableName { get; }

        public ProductionLineState State { get; private set; }

        public Option<Func<Option<Exception>>> ShutDownForMaintenance { get; private set; } = Option.None<Func<Option<Exception>>>();

        public ProductionLine(ProductionLineId id, string humanReadableName, ProductionLineState state)
        {
            Id = id;
            HumanReadableName = humanReadableName;
            State = state;
            SetAvailableActions();
        }

        private void SetAvailableActions()
        {
            ShutDownForMaintenance = IsAvailable_ShutdShutDownForMaintenance() ? Option.Some<Func<Option<Exception>>>(Execute_ShutdShutDownForMaintenance) : Option.None<Func<Option<Exception>>>();
        }

        private bool IsAvailable_ShutdShutDownForMaintenance()
        {
            return this.State == ProductionLineState.Idle;
        }

        private Option<Exception> Execute_ShutdShutDownForMaintenance()
        {
            this.State = ProductionLineState.OutOfOrder;
            SetAvailableActions();
            return Option.None<Exception>();
        }
    }
}

    