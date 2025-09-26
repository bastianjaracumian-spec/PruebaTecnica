using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAtencionesMédicas.Utils
{
    public class JWTUtils
    {
        private readonly ILogger<JWTUtils> _logger;
        private readonly IConfiguration _config;
        public JWTUtils(ILogger<JWTUtils> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public string CreateJWTUser(JWTUserDAO jWTUserDAO)
        {
            try
            {
                _logger.LogDebug("Inicia creacion de token");
                var claims = new[]
                                     {
                        new Claim(ClaimTypes.Name, "ApiAtencionesMédicas"),
                        new Claim("data", JsonConvert.SerializeObject(jWTUserDAO).ToString())
                };
                //GENERAR EL TOKEN
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("TokenKey").Value));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays( Convert.ToDouble(_config.GetSection("DaysExpiredToken").Value)),
                    SigningCredentials = credenciales
                    
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenCreate = tokenHandler.CreateToken(tokenDescriptor);
                var tokenWriten = tokenHandler.WriteToken(tokenCreate);
                return tokenWriten;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }
        public JWTUserDAO DecodeJWTUser(string Authorization)
        {
            try
            {
                Authorization = Authorization.Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(Authorization) as JwtSecurityToken;
                var data = tokenS.Claims.First(claim => claim.Type == "data").Value;

                var mUserData = JsonConvert.DeserializeObject<JWTUserDAO>(data);

                return mUserData;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
