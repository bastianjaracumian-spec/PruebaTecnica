using ApiAtencionesMédicas.Common;
using ApiAtencionesMédicas.Messages.Response;
using ApiAtencionesMédicas.Services.AppointmentServices;
using ApiAtencionesMédicas.Services.JWTServices.JWTServices;
using ApiAtencionesMédicas.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ApiAtencionesMédicas.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentServices _IAppointmentServices;
        private readonly IConfiguration _config;
        private readonly JWTUtils _JWTUtils;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentServices IAppointmentServices, JWTUtils jWTUtils)
        {
            _logger = logger;
            _IAppointmentServices = IAppointmentServices;
            _JWTUtils = jWTUtils;

        }
        /// <summary>
        /// Busqueda total de de agendas
        /// </summary>
        /// <param name="Authorization"></param>
        /// <returns>Retorna pacientes</returns>
        /// <response code="200">Retorna lista de pacientes</response>
        /// <response code="500">Error interno</response>
        [HttpGet("GetAppointment")]
        public async Task<IActionResult> GetUsers([FromHeader] string Authorization)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var users = await _IAppointmentServices.sp_ListAppointmentServices();

                return StatusCode(Constants.StatusCode.status200, new DataResponse { Data = users });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error: " + ex.Message);
                return StatusCode(Constants.StatusCode.status500, Constants.ResponseMessage.ErrorInterno);
            }
        }
    }
}
