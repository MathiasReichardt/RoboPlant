using Microsoft.AspNetCore.Mvc;
using RoboPlant.Server.REST.EntryPoint;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.ProductionLines
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLinesController : Controller
    {
        [HttpGetHypermediaObject(typeof(ProductionLinesHto))]
        public ActionResult GetProductionLines()
        {
            return Ok(new ProductionLinesHto());
        }

    }
}