using ApiAtencionesMédicas.Models.Context;
using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace ApiAtencionesMédicas.Repositorys.PatientRepository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<PatientRepository> _logger;
       
        public object Contants { get; private set; }
        public PatientRepository(UnitOfWork _unitOfWork, ILogger<PatientRepository> logger)
        {
            this._unitOfWork = _unitOfWork;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<PatientDAO>> sp_ListPatientsRepository()
        {
            try
            {
                
                List<PatientDAO> jWTUserDAOs = new List<PatientDAO>();
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_ListPatients";
                    command.CommandType = CommandType.StoredProcedure;
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                   
                    foreach (DataRow item in ds.Rows)
                    {
                        PatientDAO jwtDAO = new PatientDAO();
                        jwtDAO.Patient_Id = Convert.ToInt32(item.ItemArray[0].ToString());
                        jwtDAO.Patient_FirstName = item.ItemArray[1].ToString();
                        jwtDAO.Patient_LastName = item.ItemArray[2].ToString();
                        jwtDAO.Patient_RUT = item.ItemArray[3].ToString();
                        jwtDAO.Patient_DateOfBirth = string.IsNullOrEmpty(item.ItemArray[4].ToString()) ? null : Convert.ToDateTime(item.ItemArray[4].ToString());
                        jwtDAO.Patient_Gender = item.ItemArray[5].ToString();
                        jwtDAO.Patient_Phone = item.ItemArray[6].ToString();
                        jwtDAO.Patient_Email = item.ItemArray[7].ToString();
                        jwtDAO.Patient_AddressLine1 = item.ItemArray[8].ToString();
                        jwtDAO.Patient_AddressLine2 = item.ItemArray[9].ToString();
                        jwtDAO.Patient_City = item.ItemArray[10].ToString();
                        jwtDAO.Patient_State = item.ItemArray[11].ToString();
                        jwtDAO.Patient_PostalCode = item.ItemArray[12].ToString();
                        jwtDAO.Patient_CreatedBy = item.ItemArray[13].ToString();
                        jwtDAO.Patient_CreatedAt = string.IsNullOrEmpty(item.ItemArray[14].ToString()) ? null : Convert.ToDateTime(item.ItemArray[14].ToString());
                        jwtDAO.Patient_ModifiedBy = item.ItemArray[15].ToString();
                        jwtDAO.Patient_ModifiedAt = string.IsNullOrEmpty(item.ItemArray[16].ToString()) ? null : Convert.ToDateTime(item.ItemArray[16].ToString());
                        jwtDAO.Patient_Active = Convert.ToBoolean(item.ItemArray[17].ToString());
                        jWTUserDAOs.Add(jwtDAO);
                    }
                }
                return jWTUserDAOs;

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
        /// <returns></returns>
        public async Task<int?> sp_AddPatientRepository(PatientDTO patientDTO, string Patient_CreatedBy)
        {
            try
            {
                int? output = null;
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_AddPatients";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Patient_FirstName", SqlDbType.NVarChar) { Value = patientDTO.Patient_FirstName });
                    command.Parameters.Add(new SqlParameter("@Patient_LastName", SqlDbType.NVarChar) { Value = patientDTO.Patient_LastName });
                    command.Parameters.Add(new SqlParameter("@Patient_RUT", SqlDbType.VarChar) { Value = patientDTO.Patient_RUT});
                    command.Parameters.Add(new SqlParameter("@Patient_DateOfBirth", SqlDbType.DateTime) { Value = patientDTO.Patient_DateOfBirth});
                    command.Parameters.Add(new SqlParameter("@Patient_Gender", SqlDbType.NVarChar) { Value = patientDTO.Patient_Gender });
                    command.Parameters.Add(new SqlParameter("@Patient_Phone", SqlDbType.NVarChar) { Value = patientDTO.Patient_Phone });
                    command.Parameters.Add(new SqlParameter("@Patient_Email", SqlDbType.NVarChar) { Value = patientDTO.Patient_Email });
                    command.Parameters.Add(new SqlParameter("@Patient_AddressLine1", SqlDbType.NVarChar) { Value = patientDTO.Patient_AddressLine1 });
                    command.Parameters.Add(new SqlParameter("@Patient_AddressLine2", SqlDbType.NVarChar) { Value = patientDTO.Patient_AddressLine2 });
                    command.Parameters.Add(new SqlParameter("@Patient_City", SqlDbType.NVarChar) { Value = patientDTO.Patient_City });
                    command.Parameters.Add(new SqlParameter("@Patient_State", SqlDbType.NVarChar) { Value = patientDTO.Patient_State });
                    command.Parameters.Add(new SqlParameter("@Patient_PostalCode", SqlDbType.NVarChar) { Value = patientDTO.Patient_PostalCode });
                    command.Parameters.Add(new SqlParameter("@Patient_CreatedBy", SqlDbType.NVarChar) { Value = Patient_CreatedBy });
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        output =  Convert.ToInt32(item.ItemArray[0].ToString());
  
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
        public async Task<int?> sp_UpdatePatientRepository(PatientDTO patientDTO, string Patient_ModifiedBy, int Patient_Id)
        {
            try
            {
                int? output = null;
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_UpdatePatients";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Patient_Id", SqlDbType.Int) { Value = Patient_Id });
                    command.Parameters.Add(new SqlParameter("@Patient_FirstName", SqlDbType.NVarChar) { Value = patientDTO.Patient_FirstName });
                    command.Parameters.Add(new SqlParameter("@Patient_LastName", SqlDbType.NVarChar) { Value = patientDTO.Patient_LastName });
                    command.Parameters.Add(new SqlParameter("@Patient_DateOfBirth", SqlDbType.DateTime) { Value = patientDTO.Patient_DateOfBirth });
                    command.Parameters.Add(new SqlParameter("@Patient_Gender", SqlDbType.NVarChar) { Value = patientDTO.Patient_Gender });
                    command.Parameters.Add(new SqlParameter("@Patient_Phone", SqlDbType.NVarChar) { Value = patientDTO.Patient_Phone });
                    command.Parameters.Add(new SqlParameter("@Patient_Email", SqlDbType.NVarChar) { Value = patientDTO.Patient_Email });
                    command.Parameters.Add(new SqlParameter("@Patient_AddressLine1", SqlDbType.NVarChar) { Value = patientDTO.Patient_AddressLine1 });
                    command.Parameters.Add(new SqlParameter("@Patient_AddressLine2", SqlDbType.NVarChar) { Value = patientDTO.Patient_AddressLine2 });
                    command.Parameters.Add(new SqlParameter("@Patient_City", SqlDbType.NVarChar) { Value = patientDTO.Patient_City });
                    command.Parameters.Add(new SqlParameter("@Patient_State", SqlDbType.NVarChar) { Value = patientDTO.Patient_State });
                    command.Parameters.Add(new SqlParameter("@Patient_PostalCode", SqlDbType.NVarChar) { Value = patientDTO.Patient_PostalCode });
                    command.Parameters.Add(new SqlParameter("@Patient_ModifiedBy", SqlDbType.NVarChar) { Value = Patient_ModifiedBy });
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
        public async Task<int?> sp_DeletePatientRepository(int Patient_Id)
        {
            try
            {
                int? output = null;
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_DeletePatients";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Patient_Id", SqlDbType.Int) { Value = Patient_Id });
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

