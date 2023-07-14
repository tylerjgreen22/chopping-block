using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Error controller to be redirected to when an endpoint that doesnt exist is visited. Returns an API error response
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}