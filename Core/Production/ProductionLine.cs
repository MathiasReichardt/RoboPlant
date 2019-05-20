using System;
using Optional;
using RoboPlant.Util.PatternMatching;

namespace RoboPlant.Domain.Production
{
    public class ProductionLine
    {
        public ProductionLineId Id { get; }

        public string HumanReadableName { get; }

        public ProductionLineState State { get; private set; }

        public Option<Func<Result<Exception>>> ShutDownForMaintenance { get; private set; } = Option.None<Func<Result<Exception>>>();

        public ProductionLine(ProductionLineId id, string humanReadableName, ProductionLineState state)
        {
            Id = id;
            HumanReadableName = humanReadableName;
            State = state;
            SetAvailableActions();
        }

        private void SetAvailableActions()
        {
            ShutDownForMaintenance = IsAvailable_ShutdShutDownForMaintenance() ? Option.Some<Func<Result<Exception>>>(Execute_ShutdShutDownForMaintenance) : Option.None<Func<Result<Exception>>>();
        }

        private bool IsAvailable_ShutdShutDownForMaintenance()
        {
            return this.State == ProductionLineState.Idle;
        }

        private Result<Exception> Execute_ShutdShutDownForMaintenance()
        {
            this.State = ProductionLineState.OutOfOrder;
            SetAvailableActions();
            return new Result<Exception>.Success();
        }
    }
}

    