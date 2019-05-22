using System;
using System.Threading.Tasks;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Production.Results;
using RoboPlant.Domain.Production;
using RoboPlant.Util.PatternMatching;

namespace RoboPlant.Application.Production
{
    public class CompleteMaintenanceCommandHandler
    {
        private readonly IProductionLineRepository productionLineRepository;

        public CompleteMaintenanceCommandHandler(IProductionLineRepository productionLineRepository)
        {
            this.productionLineRepository = productionLineRepository;
        }

        public async Task<CompleteMaintenanceResult> CompleteMaintenance(Guid productionLineId)
        {
            var getProductionLineResult = await this.productionLineRepository.GetById(new ProductionLineId(productionLineId));

            var result = await getProductionLineResult.Match<Task<CompleteMaintenanceResult>>(
                async success => await ExecuteCompleteMaintenance(success.Result),
                async notFound => await Task.FromResult<CompleteMaintenanceResult>(new CompleteMaintenanceResult.NotFound()),
                async notReachablereachable => await Task.FromResult<CompleteMaintenanceResult>(new CompleteMaintenanceResult.NotReachable()),
                async error => await Task.FromResult<CompleteMaintenanceResult>(new CompleteMaintenanceResult.Error(error.Exception)));

            return result;
        }

        private async Task<CompleteMaintenanceResult> ExecuteCompleteMaintenance(ProductionLine productionLine)
        {
            var result = await productionLine.CompleteMaintenance.Match<Task<CompleteMaintenanceResult>>(
                async action => await InvokeCompleteMaintenance(action, productionLine),
                async () => await Task.FromResult(new CompleteMaintenanceResult.NotAvailable()));

            return result;
        }
        
        private async Task<CompleteMaintenanceResult> InvokeCompleteMaintenance(Func<Result<Exception>> action, ProductionLine productionLine)
        {
            return await action().Match(
                success: async _ =>
                {
                    var addResult = await this.productionLineRepository.Add(productionLine);
                    var shutDownProductionLineResult = addResult.Match<CompleteMaintenanceResult>(
                        success => new CompleteMaintenanceResult.Success(),
                        notReachable => new CompleteMaintenanceResult.NotReachable(),
                        error => new CompleteMaintenanceResult.Error(error.Exception)
                    );
                    return shutDownProductionLineResult;
                },
                failure: failure => Task.FromResult<CompleteMaintenanceResult>(new CompleteMaintenanceResult.Error(failure.Error))
            );
        }
    }
}
