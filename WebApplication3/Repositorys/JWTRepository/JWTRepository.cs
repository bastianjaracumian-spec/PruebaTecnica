using ApiAtencionesMédicas.Models.Context;
using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Repositorys.JWTRepository.JWTRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiAtencionesMédicas.Repositorys.JWTRepository.JWTRepository
{
    public class JWTRepository : IJWTRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<JWTRepository> _logger;
        JWTUserDAO jwtDAO = new JWTUserDAO();
        public object Contants { get; private set; }
        public JWTRepository(UnitOfWork _unitOfWork, ILogger<JWTRepository> logger)
        {
            this._unitOfWork = _unitOfWork;
            _logger = logger;
        }

      

        public async Task<JWTUserDAO> sp_GetUserRepository(string User, string Pass, string TypeUser)
        {
            try
            {
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_GetUser";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar) { Value = User });
                    command.Parameters.Add(new SqlParameter("@Pass", SqlDbType.VarChar) { Value = Pass });
                    command.Parameters.Add(new SqlParameter("@TypeUser", SqlDbType.NVarChar) { Value = TypeUser });
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        jwtDAO.User_Id = Convert.ToInt32(item.ItemArray[0].ToString());
                        jwtDAO.User_FirtName = item.ItemArray[1].ToString();
                        jwtDAO.User_LastName = item.ItemArray[2].ToString();
                        jwtDAO.User_Email = item.ItemArray[3].ToString();
                        jwtDAO.User_Phone = item.ItemArray[4].ToString();
                        jwtDAO.User_RUT = item.ItemArray[5].ToString();
                        jwtDAO.User_Active = Convert.ToBoolean(item.ItemArray[6].ToString());
                    }
                }
                return jwtDAO;

            }
            catch (Exception ex)
            {
                _logger.LogError("error:" + ex.Message);
                throw;
            }
            finally
            {
                _unitOfWork._context.Database.CloseConnection();
            }
        }
    }
}
