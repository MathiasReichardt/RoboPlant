using Microsoft.AspNetCore.Mvc;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.ProductionLine
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : Controller
    {
        [HttpGetHypermediaObject(typeof(ProductionLineHto))]
        public ActionResult GetProductionLines()
        {
            return Ok(null);
        }

    }
}