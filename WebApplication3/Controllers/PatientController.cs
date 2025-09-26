using ApiAtencionesMédicas.Common;
using ApiAtencionesMédicas.Messages.Response;
using ApiAtencionesMédicas.Services.PatientServices;
using ApiAtencionesMédicas.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;
using ApiAtencionesMédicas.Utils;
using Newtonsoft.Json.Linq;
using ApiAtencionesMédicas.Models.DTO;
namespace ApiAtencionesMédicas.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientServices _patientServices;
        private readonly JWTUtils _JWTUtils;
        public PatientController(ILogger<PatientController> logger, IPatientServices patientServices, JWTUtils JWTUtils)
        {
            _logger = logger;
            _patientServices = patientServices;
            _JWTUtils = JWTUtils;
        }
        /// <summary>
        /// Busqueda total de usuarios
        /// </summary>
        /// <param name="Authorization"></param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromHeader] string Authorization)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var users = await _patientServices.sp_GetUserServices();

                return StatusCode(Constants.StatusCode.status200, new DataResponse { Data = users });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error: " + ex.Message);
                return StatusCode(Constants.StatusCode.status500, Constants.ResponseMessage.ErrorInterno);
            }
        }
        /// <summary>
        /// Busqueda de usuario por rut
        /// </summary>
        /// <param name="Authorization"></param>
        /// <param name="Patient_RUT"></param>
        /// <returns></returns>
        [HttpGet("GetUserByRut")]
        public async Task<IActionResult> GetUserByRut([FromHeader] string Authorization, [FromQuery]string Patient_RUT)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var users = await _patientServices.sp_GetUserServicesByRutServices(Patient_RUT);

                return StatusCode(Constants.StatusCode.status200, new DataResponse { Data = users });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error: " + ex.Message);
                return StatusCode(Constants.StatusCode.status500, Constants.ResponseMessage.ErrorInterno);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Authorization"></param>
        /// <param name="patientDTO"></param>
        /// <returns></returns>
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromHeader] string Authorization, [FromBody] PatientDTO patientDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var user = await _patientServices.sp_AddPatientsServices(patientDTO, TokenData.User_Id.ToString());

                if (user == null || user == 0)
                    return StatusCode(Constants.StatusCode.status400, new DataResponse { Data = "Usuario no creado, verificar si ya existe" });

                return StatusCode(Constants.StatusCode.status201, new DataResponse { Data = Constants.ResponseMessage.InsercionCorrecta });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error: " + ex.Message);
                return StatusCode(Constants.StatusCode.status500, Constants.ResponseMessage.ErrorInterno);
            }
        }
        [HttpPatch("UpdatePatient/{Patient_Id}")]
        public async Task<IActionResult> UpdatePatient([FromHeader] string Authorization, [FromBody] PatientDTO patientDTO,int Patient_Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var user = await _patientServices.sp_UpdatePatientsServices(patientDTO, TokenData.User_Id.ToString(), Patient_Id);

                if (user == null || user == 0)
                    return StatusCode(Constants.StatusCode.status400, new DataResponse { Data = "Usuario no actualizado, verificar si existe" });

                return StatusCode(Constants.StatusCode.status200, new DataResponse { Data = Constants.ResponseMessage.ActualizacionCorrecta });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error: " + ex.Message);
                return StatusCode(Constants.StatusCode.status500, Constants.ResponseMessage.ErrorInterno);
            }
        }
        [HttpDelete("DeletePatient/{Patient_Id}")]
        public async Task<IActionResult> DeletePatient([FromHeader] string Authorization, int Patient_Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var user = await _patientServices.sp_DeletePatientServices(Patient_Id);

                if (user == null || user == 0)
                    return StatusCode(Constants.StatusCode.status400, new DataResponse { Data = "Usuario no actualizado, verificar si existe" });

                return StatusCode(Constants.StatusCode.status200, new DataResponse { Data = Constants.ResponseMessage.EliminaciónCorrecta });
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error: " + ex.Message);
                return StatusCode(Constants.StatusCode.status500, Constants.ResponseMessage.ErrorInterno);
            }
        }

    }
}
