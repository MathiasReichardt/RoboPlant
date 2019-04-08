using System.Collections.Generic;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.Persistence
{
    public interface IProductionLineRepository
    {
        Task<ICollection<ProductionLine>> GetAll();

        Task<GetByIdResult<ProductionLine>> GetById(ProductionLineId productionLineId);
    }
}