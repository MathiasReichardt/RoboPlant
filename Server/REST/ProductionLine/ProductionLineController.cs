﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboPlant.Application.Persistence.Results;
using RoboPlant.Application.Production;
using RoboPlant.Server.Problems;
using Util.PatternMatching;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;

namespace RoboPlant.Server.REST.ProductionLine
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : Controller
    {
        private ProductionLineCommandHandler CommandHandler { get; }

        private IProblemFactory ProblemFactory { get; }

        public ProductionLineController(ProductionLineCommandHandler commandHandler, IProblemFactory problemFactory)
        {
            CommandHandler = commandHandler;
            ProblemFactory = problemFactory;
        }

        [HttpGetHypermediaObject("{productionLineId}", typeof(ProductionLineHto))]
        public async Task<ActionResult> GetProductionLinesAsync(Guid productionLineId)
        {
            var byIdResult = await this.CommandHandler.GetById(productionLineId);


            return byIdResult.Match(
                success => Ok(new ProductionLineHto(success.Result)),
                notFound => this.Problem(ProblemFactory.EntityNotFound(typeof(ProductionLineHto).Name, productionLineId.ToString())),
                notReachable => this.Problem(ProblemFactory.ServiceUnavailable()),
                error => this.Problem(ProblemFactory.Exception(error.Exception)));
        }
    }
}