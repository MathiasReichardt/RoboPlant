using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.ProductionLine
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : Controller
    {
        [HttpGetHypermediaObject("{productionLineId}", typeof(ProductionLineHto))]
        public ActionResult GetProductionLines(Guid productionLineId)
        {
            throw new NotImplementedException();
            return Ok(null);
        }

    }
}