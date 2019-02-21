using System;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.ProductionLines;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.ProductionLine
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : Controller
    {
        private ProductionLineCommandHandler CommandHandler { get; }

        public ProductionLineController(ProductionLineCommandHandler commandHandler)
        {
            CommandHandler = commandHandler;
        }

        [HttpGetHypermediaObject("{productionLineId}", typeof(ProductionLineHto))]
        public async System.Threading.Tasks.Task<ActionResult> GetProductionLinesAsync(Guid productionLineId)
        {
            var productionLine = await this.CommandHandler.GetById(productionLineId);
            var result = new ProductionLineHto(productionLine);
            return Ok(result);
        }

    }
}