using Microsoft.AspNetCore.Mvc;

namespace ApiAtencionesMédicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Metodo()
        {
            return Ok();
        }
       
    }
}
