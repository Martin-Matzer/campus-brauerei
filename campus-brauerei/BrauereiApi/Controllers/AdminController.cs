using Microsoft.AspNetCore.Mvc;

namespace HelloCampus02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Geheime Admin-Daten der Brauerei");
        }
    }
}
