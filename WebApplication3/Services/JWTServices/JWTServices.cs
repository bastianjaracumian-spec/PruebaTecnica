using ApiAtencionesMédicas.Controllers;
using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Repositorys.JWTRepository.JWTRepository;
namespace ApiAtencionesMédicas.Services.JWTServices.JWTServices
{
    public class JWTServices : IJWTServices
    {
        private readonly ILogger<JWTServices> _logger;
        private readonly IJWTRepository _IJWTRepository;

        public JWTServices(ILogger<JWTServices> logger, IJWTRepository IJWTRepository)
        {
            _logger = logger;
            _IJWTRepository = IJWTRepository;
        }
        public async Task<JWTUserDAO> sp_GetUserServices(string User, string Pass, string TypeUser)
        {
            return  await _IJWTRepository.sp_GetUserRepository( User,Pass,TypeUser);
        }
    }
}
