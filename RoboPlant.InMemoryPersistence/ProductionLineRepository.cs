using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Domain.Production;
using RoboPlant.InMemoryPersistence.Models;
using SharpRepository.InMemoryRepository;

namespace RoboPlant.InMemoryPersistence
{
    public class ProductionLineRepository : IProductionLineRepository
    {
        private readonly InMemoryRepository<ProductionLineModel, Guid> internalRepository;

        public ProductionLineRepository()
        {
            this.internalRepository = new InMemoryRepository<ProductionLineModel, Guid>();
            this.internalRepository.GenerateKeyOnAdd = true;
            AddDemoData();
        }

        private void AddDemoData()
        {
            var insertItems = new List<ProductionLineModel>
            {
                new ProductionLineModel
                {
                    Id = Guid.NewGuid(),
                    HumanReadableName = "Line 1",
                    State = ProductionLineState.Idle
                },
                new ProductionLineModel
                {
                    Id = Guid.NewGuid(),
                    HumanReadableName = "Line 2",
                    State = ProductionLineState.Idle
                },
                new ProductionLineModel
                {
                    Id = Guid.NewGuid(),
                    HumanReadableName = "Line 3",
                    State = ProductionLineState.Idle
                }
            };
          
            this.internalRepository.Add(insertItems);
            
        }

        public Task<ICollection<ProductionLine>> GetAll()
        {
            var result = this.internalRepository
                .GetAll()
                .Select(m => new ProductionLine(new ProductionLineId(m.Id), m.HumanReadableName, m.State))
                .ToList();
            return Task.FromResult<ICollection<ProductionLine>>(result);
        }

        public Task<ProductionLine> GetById()
        {
            throw new NotImplementedException();
        }
    }
}
