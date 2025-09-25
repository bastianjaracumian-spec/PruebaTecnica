using ApiAtencionesMédicas.Models.DAO;

namespace ApiAtencionesMédicas.Repositorys.JWTRepository.JWTRepository
{
    public interface IJWTRepository
    {
        Task<JWTUserDAO> sp_GetUserRepository(string User, string Pass, string TypeUser);
    }
}
