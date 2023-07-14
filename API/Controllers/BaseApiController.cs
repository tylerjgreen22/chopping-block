using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Base API controller that other controllers can derive from
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}