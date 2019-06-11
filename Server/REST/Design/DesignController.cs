using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.Design;
using RoboPlant.Server.Problems;
using WebApi.HypermediaExtensions.Hypermedia.Actions;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;

namespace RoboPlant.Server.REST.Design
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignController : Controller
    {
        private IProblemFactory ProblemFactory { get; }

        private DesignCommandHandler CommandHandler { get; }

        public DesignController(DesignCommandHandler commandHandler, IProblemFactory problemFactory)
        {
            ProblemFactory = problemFactory;
            this.CommandHandler = commandHandler;
        }

        [HttpGetHypermediaObject(typeof(DesignHto))]
        public ActionResult GetProductionLines()
        {
            return Ok(new DesignHto());
        }

        [HttpPostHypermediaAction("Queries", typeof(HypermediaAction<BlueprintQueryParameters>))]
        public ActionResult NewQuery(BlueprintQueryParameters queryParameters)
        {
            if (queryParameters == null)
            {
                return this.Problem(ProblemFactory.BadParameters());
            }

            var canQueryResult = CommandHandler.CanQueryBlueprints();
            return canQueryResult.Match(
                // Will create a Location header with a URI to the result.
                available => this.CreatedQuery(typeof(BlueprintQueryResultHto), queryParameters),
                notAvailable => this.Problem(ProblemFactory.OperationNotAvailable()),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception))
            );
        }

        [HttpGetHypermediaObject("Query", typeof(BlueprintQueryResultHto))]
        public async Task<ActionResult> Query([FromQuery] BlueprintQueryParameters queryParameters)
        {
            if (queryParameters == null)
            {
                return this.Problem(ProblemFactory.BadParameters());
            }

            var blueprintQuery = new BlueprintsQuery
            {
                MaxProductionEfford = queryParameters.MaxProductionEfford,
                NameContains = queryParameters.ModelName
            };

            var result = await CommandHandler.Query(blueprintQuery);

            return result.Match(
                success =>
                {
                    var resultHto = Ok(new BlueprintQueryResultHto(success.Result, queryParameters));
                    return resultHto;
                },
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                invalidQuery => this.Problem(ProblemFactory.BadParameters()),
                error => this.Problem(ProblemFactory.Exception(error.Exception))
            );
        }
    }
}