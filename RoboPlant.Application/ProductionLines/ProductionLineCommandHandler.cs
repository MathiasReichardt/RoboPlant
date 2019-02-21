using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.ProductionLines
{
    public class ProductionLineCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public ProductionLineCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public Task<ProductionLine> GetById(Guid productionLineId)
        {
            var result = this.productionLineRepository.GetById(new ProductionLineId(productionLineId));
            return result;
        }

    }
}
