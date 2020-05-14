using System;
using RoboPlant.Domain.Ids;

namespace RoboPlant.Domain.Production
{
    public class ProductionLineId : IdBase
    {
        public ProductionLineId(Guid value) : base(value)
        {
        }
    }
}