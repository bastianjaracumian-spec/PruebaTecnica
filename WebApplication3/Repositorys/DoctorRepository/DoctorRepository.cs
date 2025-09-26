using ApiAtencionesMédicas.Models.Context;
using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiAtencionesMédicas.Repositorys.DoctorRepository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<DoctorRepository> _logger;

        public object Contants { get; private set; }
        public DoctorRepository(UnitOfWork _unitOfWork, ILogger<DoctorRepository> logger)
        {
            this._unitOfWork = _unitOfWork;
            _logger = logger;
        }
        public async Task<List<DoctorDAO>> sp_ListDoctorsRepository()
        {
            try
            {

                List<DoctorDAO> doctorDAOs = new List<DoctorDAO>();
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_ListDoctors";
                    command.CommandType = CommandType.StoredProcedure;
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        DoctorDAO doctorDAO = new DoctorDAO();
                        doctorDAO.Doctor_Id = Convert.ToInt32(item.ItemArray[0].ToString());
                        doctorDAO.Doctor_FirstName = item.ItemArray[1].ToString();
                        doctorDAO.Doctor_LastName = item.ItemArray[2].ToString();
                        doctorDAO.Doctor_Email = item.ItemArray[3].ToString();
                        doctorDAO.Doctor_Phone = item.ItemArray[4].ToString();
                        doctorDAO.Doctor_LicenseNumber = item.ItemArray[5].ToString();
                        doctorDAO.SpecialityId = item.ItemArray[6].ToString();
                        doctorDAO.Doctor_CreatedBy = item.ItemArray[7].ToString();
                        doctorDAO.Doctor_CreatedAt = string.IsNullOrEmpty(item.ItemArray[8].ToString()) ? null : Convert.ToDateTime(item.ItemArray[8].ToString());
                        doctorDAO.Doctor_ModifiedBy = item.ItemArray[9].ToString();
                        doctorDAO.Doctor_ModifiedAt = string.IsNullOrEmpty(item.ItemArray[10].ToString()) ? null : Convert.ToDateTime(item.ItemArray[10].ToString());
                        doctorDAO.Doctor_Active = Convert.ToBoolean(item.ItemArray[11].ToString());
                        doctorDAOs.Add(doctorDAO);
                    }
                }
                return doctorDAOs;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientDTO"></param>
        /// <param name="Patient_CreatedBy"></param>
        /// <returns></returns>
        public async Task<int?> sp_AddDoctorRepository(DoctorDTO doctorDTO, string Doctor_CreatedBy)
        {
            try
            {
                int? output = null;
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_AddDoctors";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Doctor_FirstName", SqlDbType.NVarChar) { Value = doctorDTO.Doctor_FirstName });
                    command.Parameters.Add(new SqlParameter("@Doctor_LastName", SqlDbType.NVarChar) { Value = doctorDTO.Doctor_LastName });
                    command.Parameters.Add(new SqlParameter("@Doctor_Email", SqlDbType.NVarChar) { Value = doctorDTO.Doctor_Email });
                    command.Parameters.Add(new SqlParameter("@Doctor_Phone", SqlDbType.NVarChar) { Value = doctorDTO.Doctor_Phone });
                    command.Parameters.Add(new SqlParameter("@Doctor_LicenseNumber", SqlDbType.NVarChar) { Value = doctorDTO.Doctor_LicenseNumber });
                    command.Parameters.Add(new SqlParameter("@SpecialityId", SqlDbType.Int) { Value = doctorDTO.SpecialityId });
                    command.Parameters.Add(new SqlParameter("@Doctor_CreatedBy", SqlDbType.NVarChar) { Value = Doctor_CreatedBy });
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        output = Convert.ToInt32(item.ItemArray[0].ToString());

                    }
                }
                return output;

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
        public async Task<int?> sp_UpdateDoctorRepository(DoctorDTO doctorDTO, string Doctor_ModifiedBy, int Doctor_Id)
        {
            try
            {
                int? output = null;
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_UpdateDoctor";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Doctor_Id", SqlDbType.Int) { Value = Doctor_Id });
                    command.Parameters.Add(new SqlParameter("@Doctor_FirstName", SqlDbType.VarChar) { Value = doctorDTO.Doctor_FirstName });
                    command.Parameters.Add(new SqlParameter("@Doctor_LastName", SqlDbType.VarChar) { Value = doctorDTO.Doctor_LastName });
                    command.Parameters.Add(new SqlParameter("@Doctor_Email", SqlDbType.VarChar) { Value = doctorDTO.Doctor_Email });
                    command.Parameters.Add(new SqlParameter("@Doctor_Phone", SqlDbType.VarChar) { Value = doctorDTO.Doctor_Phone });
                    command.Parameters.Add(new SqlParameter("@SpecialityId", SqlDbType.VarChar) { Value = doctorDTO.SpecialityId });
                    command.Parameters.Add(new SqlParameter("@Doctor_ModifiedBy", SqlDbType.VarChar) { Value = Doctor_ModifiedBy });
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        output = Convert.ToInt32(item.ItemArray[0].ToString());
                    }
                }
                return output;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Patient_Id"></param>
        /// <returns></returns>
        public async Task<int?> sp_DeleteDoctorRepository(int Doctor_Id)
        {
            try
            {
                int? output = null;
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_DeleteDoctor";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Doctor_Id", SqlDbType.Int) { Value = Doctor_Id });
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        output = Convert.ToInt32(item.ItemArray[0].ToString());
                    }
                }
                return output;

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
