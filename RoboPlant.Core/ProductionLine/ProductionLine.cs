namespace RoboPlant.Domain.ProductionLine
{
    public class ProductionLine
    {
        public string HumanReadableName { get; }

        public ProductionLineState State { get; }

        public ProductionLine(string humanReadableName, ProductionLineState state)
        {
            HumanReadableName = humanReadableName;
            State = state;
        }
    }
}