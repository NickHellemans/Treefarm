using Microsoft.AspNetCore.Mvc;

namespace AP.MyTreeFarm.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class APIv1Controller : ControllerBase
    {
    }
}
