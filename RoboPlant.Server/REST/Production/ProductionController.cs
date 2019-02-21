using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.Production;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.Production
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : Controller
    {
        private readonly ProductionCommandHandler commandHandler;

        public ProductionController(ProductionCommandHandler commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        [HttpGetHypermediaObject(typeof(ProductionHto))]
        public async Task<ActionResult> GetProductionLines()
        {
            var productionLines = await commandHandler.GetAllProductionLines();
            var result = new ProductionHto(productionLines);
            return Ok(result);
        }

    }
}