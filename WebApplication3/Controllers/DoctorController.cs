using Microsoft.AspNetCore.Mvc;

namespace ApiAtencionesMédicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ILogger<DoctorController> logger)
        {
            _logger = logger;
        }
        [HttpGet("obtener")]
        public async Task<IActionResult> obtener()
        {
            return Ok();
        }
    }
}
