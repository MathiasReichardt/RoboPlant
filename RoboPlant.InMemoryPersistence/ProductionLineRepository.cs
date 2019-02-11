using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Domain.ProductionLine;

namespace RoboPlant.InMemoryPersistence
{
    public class ProductionLineRepository : IProductionLineRepository
    {
        public Task<ICollection<ProductionLine>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductionLine> GetById()
        {
            throw new NotImplementedException();
        }
    }
}
