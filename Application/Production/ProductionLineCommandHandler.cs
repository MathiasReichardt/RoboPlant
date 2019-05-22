using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Application.Production.Results;
using RoboPlant.Domain.Production;
using RoboPlant.Util.PatternMatching;

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

            var result = await getProductionLineResult.Match<Task<ShutDownProductionLineResult>>(
                async success => await ExecuteAction(success.Result),
                async notFound => await Task.FromResult<ShutDownProductionLineResult>(new ShutDownProductionLineResult.NotFound()),
                async notReachablereachable => await Task.FromResult<ShutDownProductionLineResult>(new ShutDownProductionLineResult.NotReachable()),
                async error => await Task.FromResult<ShutDownProductionLineResult>(new ShutDownProductionLineResult.Error(error.Exception)));

            return result;
        }

        private async Task<ShutDownProductionLineResult> ExecuteAction(ProductionLine productionLine)
        {
            var result = await productionLine.ShutDownForMaintenance.Match<Task<ShutDownProductionLineResult>>(
                async action => await InvokeAction(action, productionLine),
                async () => await Task.FromResult(new ShutDownProductionLineResult.NotAvailable()));

            return result;
        }
        
        private async Task<ShutDownProductionLineResult> InvokeAction(Func<Result<Exception>> action, ProductionLine productionLine)
        {
            return await action().Match(
                success: async _ =>
                {
                    var addResult = await this.productionLineRepository.Add(productionLine);
                    var shutDownProductionLineResult = addResult.Match<ShutDownProductionLineResult>(
                        success => new ShutDownProductionLineResult.Success(),
                        notReachable => new ShutDownProductionLineResult.NotReachable(),
                        error => new ShutDownProductionLineResult.Error(error.Exception)
                    );
                    return shutDownProductionLineResult;
                },
                failure: failure => Task.FromResult<ShutDownProductionLineResult>(new ShutDownProductionLineResult.Error(failure.Error))
            );
        }
    }
}
