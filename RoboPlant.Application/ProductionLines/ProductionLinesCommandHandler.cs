using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.ProductionLines
{
    public class ProductionLinesCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public ProductionLinesCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public Task<ICollection<ProductionLine>> GetAllProductionLines()
        {
            var result = this.productionLineRepository.GetAll();
            return result;
        }

    }
}
