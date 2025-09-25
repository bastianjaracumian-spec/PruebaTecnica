using ApiAtencionesMédicas.Common;
using ApiAtencionesMédicas.Messages.Response;
using ApiAtencionesMédicas.Models.DTO;
using ApiAtencionesMédicas.Services.JWTServices.JWTServices;
using ApiAtencionesMédicas.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ApiAtencionesMédicas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticatorController : ControllerBase
    {
        private readonly ILogger<AuthenticatorController> _logger;
        private readonly IJWTServices _IJWTServices;
        private readonly IConfiguration _config;
        private readonly JWTUtils _jWTUtils;
        public AuthenticatorController(ILogger<AuthenticatorController> logger, IJWTServices IJWTServices, JWTUtils jWTUtils)
        {
            _logger = logger;
            _IJWTServices = IJWTServices;   
            _jWTUtils = jWTUtils;

        }
        /// <summary>
        /// Metodo para creación de token JWT por usuario
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Pass"></param>
        /// <param name="TypeUser"></param>
        /// <returns>Retorna Token</returns>
        /// <response code="201">La autenticación fue exitosa. La respuesta contendrá el objeto Usuario que fue autenticado</response>
        /// <response code="200">La autenticación fue exitosa. La respuesta contendrá el objeto Usuario que fue autenticado</response>

        [HttpGet("GetTokenUser/{User}/{Pass}/{TypeUser}")]
        public async Task<IActionResult> GetTokenUser(string User, string Pass, string TypeUser)
        {
            if (string.IsNullOrEmpty(User))
                return StatusCode(Constants.StatusCode.status400, "User es requerido");
            if (string.IsNullOrEmpty(Pass))
                return StatusCode(Constants.StatusCode.status400, "Pass es requerido");
            if (string.IsNullOrEmpty(TypeUser))
                return StatusCode(Constants.StatusCode.status400, "TypeUser es requerido");
            
            var user = await _IJWTServices.sp_GetUserServices(User, Pass, TypeUser);
            if (user.User_Id == 0)
                return StatusCode(Constants.StatusCode.status404, "User not found");
            var token = _jWTUtils.CreateJWTUser(user);
            return StatusCode(Constants.StatusCode.status201, new DataResponse { Data = token });
      
        }
    }
}
