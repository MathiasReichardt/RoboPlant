using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
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
                    Id = new Guid("EDAEF8DA-C0CC-4284-B50F-086E468A399C"),
                    HumanReadableName = "Line 1",
                    State = ProductionLineState.Idle
                },
                new ProductionLineModel
                {
                    Id = new Guid("60BF9495-E64C-4766-9E27-F7F6827F477C"),
                    HumanReadableName = "Line 2",
                    State = ProductionLineState.Idle
                },
                new ProductionLineModel
                {
                    Id = new Guid("7FC32333-19D2-4C82-9E3A-C7C6B5583194"),
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

        public Task<GetByIdResult<ProductionLine>> GetById(ProductionLineId productionLineId)
        {
            ProductionLineModel result;
            try
            {
                result = this.internalRepository.Get(productionLineId.Value);
            }
            catch (Exception e)
            {
                return Task.FromResult<GetByIdResult<ProductionLine>>(new GetByIdResult<ProductionLine>.Error(e));
            }

            if (result == null)
            {
                return Task.FromResult<GetByIdResult<ProductionLine>>(new GetByIdResult<ProductionLine>.NotFound());
            }

            var productionLine = new ProductionLine(
                new ProductionLineId(result.Id),
                result.HumanReadableName,
                result.State);
            return Task.FromResult<GetByIdResult<ProductionLine>>(new GetByIdResult<ProductionLine>.Success(productionLine));
        }
    }
}