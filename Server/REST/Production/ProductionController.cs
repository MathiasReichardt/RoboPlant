using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.Production;
using RoboPlant.Server.Problems;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;

namespace RoboPlant.Server.REST.Production
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : Controller
    {
        private IProblemFactory ProblemFactory { get; }

        private ProductionCommandHandler CommandHandler { get; }

        public ProductionController(ProductionCommandHandler commandHandler, IProblemFactory problemFactory)
        {
            ProblemFactory = problemFactory;
            this.CommandHandler = commandHandler;
        }

        [HttpGetHypermediaObject(typeof(ProductionHto))]
        public async Task<ActionResult> GetProductionLines()
        {
            var getAllResult = await CommandHandler.GetAllProductionLines();

            return getAllResult.Match(
                success => Ok(new ProductionHto(success.Result)),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception)));
        }

    }
}