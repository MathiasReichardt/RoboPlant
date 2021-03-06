﻿using System.Threading.Tasks;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.Persistence
{
    public interface IProductionLineRepository
    {
        Task<GetAllResult<ProductionLine>> GetAll();

        Task<GetByIdResult<ProductionLine>> GetById(ProductionLineId productionLineId);

        Task<AddResult<ProductionLineId>> Add(ProductionLine productionLine);
    }
}