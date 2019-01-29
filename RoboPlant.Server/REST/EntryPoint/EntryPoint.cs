using Microsoft.AspNetCore.Mvc;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

namespace RoboPlant.Server.REST.EntryPoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryPoint : Controller
    {
        [HttpGetHypermediaObject(typeof(EntryPointHto))]
        public ActionResult GetEntryPoint()
        {
            return Ok(new EntryPointHto());
        }
    }
}
