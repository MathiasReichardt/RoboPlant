using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.Production.ProductionLine;
using RoboPlant.Server.Problems;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;

namespace RoboPlant.Server.REST.ProductionLine
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : Controller
    {
        private GetByIdCommandHandler GetByIdCommandHandler { get; }
        private ShutDownForMaintenanceCommandHandler ShutDownForMaintenanceCommandHandler { get; }
        public CompleteMaintenanceCommandHandler CompleteMaintenanceCommandHandler { get; }

        private IProblemFactory ProblemFactory { get; }

        public ProductionLineController(
            GetByIdCommandHandler getByIdCommandHandler,
            ShutDownForMaintenanceCommandHandler shutDownForMaintenanceCommandHandler,
            CompleteMaintenanceCommandHandler completeMaintenanceCommandHandler,
            IProblemFactory problemFactory)
        {
            GetByIdCommandHandler = getByIdCommandHandler;
            ShutDownForMaintenanceCommandHandler = shutDownForMaintenanceCommandHandler;
            CompleteMaintenanceCommandHandler = completeMaintenanceCommandHandler;
            ProblemFactory = problemFactory;
        }

        [HttpGetHypermediaObject("{productionLineId}", typeof(ProductionLineHto))]
        public async Task<ActionResult> GetProductionLineAsync(Guid productionLineId)
        {
            var byIdResult = await GetByIdCommandHandler.GetById(productionLineId);
            return byIdResult.Match(
                success => Ok(new ProductionLineHto(success.Result)),
                notFound => this.Problem(ProblemFactory.EntityNotFound(typeof(ProductionLineHto).Name, productionLineId.ToString())),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception)));
        }

        [HttpPostHypermediaAction("{productionLineId:Guid}/ShutDownForMaintenance", typeof(ShutDownForMaintenance))]
        public async Task<ActionResult> ShutDownForMaintenance(Guid productionLineId)
        {
            var shutDownResult = await ShutDownForMaintenanceCommandHandler.ShutDownForMaintenance(productionLineId);
            return shutDownResult.Match(
                success => NoContent(),
                notAvailable => this.CanNotExecute(), 
                notFound => this.Problem(ProblemFactory.EntityNotFound(typeof(ProductionLineHto).Name, productionLineId.ToString())),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception)));
        }

        [HttpPostHypermediaAction("{productionLineId:Guid}/CompleteMaintenance", typeof(CompleteMaintenance))]
        public async Task<ActionResult> CompleteMaintenance(Guid productionLineId)
        {
            var shutDownResult = await CompleteMaintenanceCommandHandler.CompleteMaintenance(productionLineId);
            return shutDownResult.Match(
                success => NoContent(),
                notAvailable => this.CanNotExecute(),
                notFound => this.Problem(ProblemFactory.EntityNotFound(typeof(ProductionLineHto).Name, productionLineId.ToString())),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception)));
        }

        [HttpPostHypermediaAction("{productionLineId:Guid}/ProduceRobot", typeof(ProduceRobot))]
        public async Task<ActionResult> ProduceRobot(Guid productionLineId, ProductionRequestParameters parameters)
        {
            // todo
            // check if parameter is provided
            // call handler
            return Ok();
        }
    }
}