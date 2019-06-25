using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.Design;
using RoboPlant.Server.Problems;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;

namespace RoboPlant.Server.REST.RobotBlueprint
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotBlueprintController : Controller
    {
        private DesignCommandHandler CommandHandler { get; }
        
        private IProblemFactory ProblemFactory { get; }

        public RobotBlueprintController(
            DesignCommandHandler commandHandler,
            IProblemFactory problemFactory)
        {
            CommandHandler = commandHandler;
            ProblemFactory = problemFactory;
        }

        [HttpGetHypermediaObject("{robotBlueprintId:Guid}", typeof(RobotBlueprintHto))]
        public async Task<ActionResult> GetRobotBlueprint(Guid robotBlueprintId)
        {
            var byIdResult = await CommandHandler.GetRobotBlueprintGetById(robotBlueprintId);
            return byIdResult.Match(
                success => Ok(new RobotBlueprintHto(success.Result)),
                notFound => this.Problem(ProblemFactory.EntityNotFound(typeof(RobotBlueprintHto).Name, robotBlueprintId.ToString())),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception)));
        }
    }
}