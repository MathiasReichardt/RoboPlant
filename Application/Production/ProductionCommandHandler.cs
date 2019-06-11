using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;

namespace RoboPlant.Application.Production
{
    public class ProductionCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public ProductionCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public Task<GetAllResult<Domain.Production.ProductionLine>> GetAllProductionLines()
        {
            var result = productionLineRepository.GetAll();
            return result;
        }

    }
}
