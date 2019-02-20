using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.ProductionLines;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.ProductionLines
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLinesController : Controller
    {
        private readonly ProductionLinesCommandHandler commandHandler;

        public ProductionLinesController(ProductionLinesCommandHandler commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [HttpGetHypermediaObject(typeof(ProductionLinesHto))]
        public async Task<ActionResult> GetProductionLines()
        {
            var productionLines = await commandHandler.GetAllProductionLines();
            var result = new ProductionLinesHto(productionLines);
            return Ok(result);
        }

    }
}