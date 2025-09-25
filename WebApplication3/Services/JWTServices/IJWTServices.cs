using ApiAtencionesMédicas.Models.DAO;

namespace ApiAtencionesMédicas.Services.JWTServices.JWTServices
{
    public interface IJWTServices
    {
        Task<JWTUserDAO> sp_GetUserServices(string User, string Pass, string TypeUser);
    }
}
