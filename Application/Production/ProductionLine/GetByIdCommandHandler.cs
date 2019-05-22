﻿using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.Production
{
    public class GetByIdCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public GetByIdCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public Task<GetByIdResult<ProductionLine>> GetById(Guid productionLineId)
        {
            var result = this.productionLineRepository.GetById(new ProductionLineId(productionLineId));

            return result;
        }
    }
}
