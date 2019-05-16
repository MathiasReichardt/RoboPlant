using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Application.Production.Results;
using RoboPlant.Domain.Production;

namespace RoboPlant.Application.Production
{
    public class ProductionLineCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public ProductionLineCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public Task<GetByIdResult<ProductionLine>> GetById(Guid productionLineId)
        {
            var result = this.productionLineRepository.GetById(new ProductionLineId(productionLineId));

            return result;
        }

        public async Task<ShutDownProductionLineResult> ShutDownProductionLine(Guid productionLineId)
        {
            var getProductionLineResult = await this.productionLineRepository.GetById(new ProductionLineId(productionLineId));

            var result = getProductionLineResult.Match<ShutDownProductionLineResult>(
                success => ExecuteAction(success.Result),
                notFound => new ShutDownProductionLineResult.NotFound(),
                notReachablereachable => new ShutDownProductionLineResult.NotReachable(),
                error => new ShutDownProductionLineResult.Error(error.Exception));

            return result;
        }

        private static ShutDownProductionLineResult ExecuteAction(ProductionLine productionLine)
        {
            var result = productionLine.ShutDownForMaintenance.Match<ShutDownProductionLineResult>(
                action => action.Invoke().Match<ShutDownProductionLineResult>(
                        exception => new ShutDownProductionLineResult.Error(exception),
                        () => new ShutDownProductionLineResult.Success()), todo: persist
                () => new ShutDownProductionLineResult.NotAvailable());

            return result;
        }
    }
}
