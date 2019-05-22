using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Application.Production.Results;
using RoboPlant.Domain.Production;
using RoboPlant.Util.PatternMatching;

namespace RoboPlant.Application.Production
{
    public class ShutDownForMaintenanceCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public ShutDownForMaintenanceCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public async Task<ShutDownForMaintenanceResult> ShutDownForMaintenance(Guid productionLineId)
        {
            var getProductionLineResult = await this.productionLineRepository.GetById(new ProductionLineId(productionLineId));

            var result = await getProductionLineResult.Match<Task<ShutDownForMaintenanceResult>>(
                async success => await ExecuteShutDown(success.Result),
                async notFound => await Task.FromResult<ShutDownForMaintenanceResult>(new ShutDownForMaintenanceResult.NotFound()),
                async notReachablereachable => await Task.FromResult<ShutDownForMaintenanceResult>(new ShutDownForMaintenanceResult.NotReachable()),
                async error => await Task.FromResult<ShutDownForMaintenanceResult>(new ShutDownForMaintenanceResult.Error(error.Exception)));

            return result;
        }

        private async Task<ShutDownForMaintenanceResult> ExecuteShutDown(ProductionLine productionLine)
        {
            var result = await productionLine.ShutDownForMaintenance.Match<Task<ShutDownForMaintenanceResult>>(
                async action => await InvokeShutDown(action, productionLine),
                async () => await Task.FromResult(new ShutDownForMaintenanceResult.NotAvailable()));

            return result;
        }
        
        private async Task<ShutDownForMaintenanceResult> InvokeShutDown(Func<Result<Exception>> action, ProductionLine productionLine)
        {
            return await action().Match(
                success: async _ =>
                {
                    var addResult = await this.productionLineRepository.Add(productionLine);
                    var shutDownProductionLineResult = addResult.Match<ShutDownForMaintenanceResult>(
                        success => new ShutDownForMaintenanceResult.Success(),
                        notReachable => new ShutDownForMaintenanceResult.NotReachable(),
                        error => new ShutDownForMaintenanceResult.Error(error.Exception)
                    );
                    return shutDownProductionLineResult;
                },
                failure: failure => Task.FromResult<ShutDownForMaintenanceResult>(new ShutDownForMaintenanceResult.Error(failure.Error))
            );
        }
    }
}
