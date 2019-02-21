using System.Collections.Generic;
using System.Threading.Tasks;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.Persistence
{
    public interface IProductionLineRepository
    {
        Task<ICollection<ProductionLine>> GetAll();

        Task<ProductionLine> GetById(ProductionLineId productionLineId);
    }
}