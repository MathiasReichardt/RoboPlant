using Microsoft.AspNetCore.Mvc;
using WebApi.HypermediaExtensions.WebApi.AttributedRoutes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
