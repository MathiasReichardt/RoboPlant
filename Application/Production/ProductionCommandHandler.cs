using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.Production
{
    public class ProductionCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public ProductionCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public Task<GetAllResult<ProductionLine>> GetAllProductionLines()
        {
            var result = this.productionLineRepository.GetAll();
            return result;
        }

    }
}
