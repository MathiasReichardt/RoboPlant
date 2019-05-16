using System;
using Optional;
using RoboPlant.Domain.Ids;

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



    //public class Command<TParameters, TResult>
    //{
    //    private Func<bool> CanExecute { get; }

    //    private Func<TParameters, TResult> ExecuteFunc { get; }

    //    public Command(Func<bool> canExecute, Func<TParameters, TResult> execute)
    //    {
    //        CanExecute = canExecute;
    //        ExecuteFunc = execute;
    //    }

    //    public TResult Execute(TParameters parameters)
    //    {
    //        if (!this.CanExecute.Invoke())
    //        {
    //            throw new InvalidOperationException("Can not execute action but was executed.");
    //        }

    //        return this.ExecuteFunc.Invoke(parameters);
    //    }

    //}

    //public class Command<TResult>
    //{
    //    private Func<bool> CanExecute { get; }

    //    private Func<TResult> ExecuteFunc { get; }

    //    public Command(Func<bool> canExecute, Func<TResult> execute)
    //    {
    //        CanExecute = canExecute;
    //        ExecuteFunc = execute;
    //    }

    //    public bool IsPossible()
    //    {
    //        return CanExecute();
    //    }

    //    public void Match(Action<TResult> onExecuted, Action onImpossible)
    //    {

    //    }

    //    public TResult Execute()
    //    {
    //        if (!this.CanExecute.Invoke())
    //        {
    //            throw new InvalidOperationException("Can not execute action but was executed.");
    //        }

    //        return this.ExecuteFunc.Invoke();
    //    }
    //}

    public class ProductionLineId : IdBase
    {
        public ProductionLineId(Guid value) : base(value)
        {
        }
    }
}

    