using ApiAtencionesMédicas.Common;
using ApiAtencionesMédicas.Messages.Response;
using ApiAtencionesMédicas.Models.DTO;
using ApiAtencionesMédicas.Repositorys.DoctorRepository;
using ApiAtencionesMédicas.Services.DoctorServices;
using ApiAtencionesMédicas.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ApiAtencionesMédicas.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly IDoctorServices _doctorServices;
        private readonly JWTUtils _JWTUtils;
       
        public DoctorController(ILogger<DoctorController> logger,JWTUtils jWTUtils, IDoctorServices doctorServices)
        {
            _logger = logger;
            _JWTUtils = jWTUtils;
            _doctorServices = doctorServices;
        }
        /// <summary>
        /// Busqueda total de doctores
        /// </summary>
        /// <param name="Authorization"></param>
        /// <returns>Retorna pacientes</returns>
        /// <response code="200">Retorna lista de doctores</response>
        /// <response code="500">Error interno</response>
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> GetDoctors([FromHeader] string Authorization)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var users = await _doctorServices.sp_ListDoctorsServices();

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
        /// <param name="doctorDTO"></param>
        /// <returns></returns>
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromHeader] string Authorization, [FromBody] DoctorDTO doctorDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var user = await _doctorServices.sp_AddDoctorServices(doctorDTO, TokenData.User_Id.ToString());

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Authorization"></param>
        /// <param name="doctorDTO"></param>
        /// <param name="Doctor_Id"></param>
        /// <returns></returns>
        [HttpPatch("UpdateDoctor/{Doctor_Id}")]
        public async Task<IActionResult> UpdateDoctor([FromHeader] string Authorization, [FromBody] DoctorDTO doctorDTO, int Doctor_Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var user = await _doctorServices.sp_UpdateDoctorServices(doctorDTO, TokenData.User_Id.ToString(), Doctor_Id);

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Authorization"></param>
        /// <param name="Doctor_Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDoctor/{Doctor_Id}")]
        public async Task<IActionResult> DeletePatient([FromHeader] string Authorization, int Doctor_Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Authorization))
                    return StatusCode(Constants.StatusCode.status401, new DataResponse { Data = Constants.ResponseMessage.AuthRequired });
                var TokenData = _JWTUtils.DecodeJWTUser(Authorization);
                var user = await _doctorServices.sp_DeleteDoctorServices(Doctor_Id);

                if (user == null || user == 0)
                    return StatusCode(Constants.StatusCode.status400, new DataResponse { Data = "Usuario no eliminado, verificar si existe" });

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
