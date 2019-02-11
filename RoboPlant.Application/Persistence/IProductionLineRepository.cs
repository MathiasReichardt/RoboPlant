using System.Collections.Generic;
using System.Threading.Tasks;
using RoboPlant.Domain.ProductionLine;

namespace RoboPlant.Application.Persistence
{
    public interface IProductionLineRepository
    {
        Task<ICollection<ProductionLine>> GetAll();
        Task<ProductionLine> GetById();
    }
}