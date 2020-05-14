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
        public Option<Func<Result<Exception>>> CompleteMaintenance { get; private set; } = Option.None<Func<Result<Exception>>>();

        public ProductionLine(ProductionLineId id, string humanReadableName, ProductionLineState state)
        {
            Id = id;
            HumanReadableName = humanReadableName;
            State = state;
            SetAvailableActions();
        }

        private void SetAvailableActions()
        {
            ShutDownForMaintenance = IsAvailable_ShutDownForMaintenance() ? Option.Some<Func<Result<Exception>>>(Execute_ShutDownForMaintenance) : Option.None<Func<Result<Exception>>>();
            CompleteMaintenance = IsAvailable_CompleteMaintenance() ? Option.Some<Func<Result<Exception>>>(Execute_CompleteMaintenance) : Option.None<Func<Result<Exception>>>();
        }

        private bool IsAvailable_ShutDownForMaintenance()
        {
            return State == ProductionLineState.Idle;
        }

        private Result<Exception> Execute_ShutDownForMaintenance()
        {
            State = ProductionLineState.Maintenance;
            SetAvailableActions();
            return new Result<Exception>.Success();
        }

        private bool IsAvailable_CompleteMaintenance()
        {
            return State == ProductionLineState.Maintenance;
        }

        private Result<Exception> Execute_CompleteMaintenance()
        {
            State = ProductionLineState.Idle;
            SetAvailableActions();
            return new Result<Exception>.Success();
        }
    }
}

    