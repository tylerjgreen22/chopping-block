using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Fallback controller for serving static client files (index.html) from wwwroot
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
