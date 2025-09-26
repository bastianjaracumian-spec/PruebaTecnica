using ApiAtencionesMédicas.Models.Context;
using ApiAtencionesMédicas.Models.DAO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiAtencionesMédicas.Repositorys.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentRepository> _logger;

        public object Contants { get; private set; }
        public AppointmentRepository(UnitOfWork _unitOfWork, ILogger<AppointmentRepository> logger)
        {
            this._unitOfWork = _unitOfWork;
            _logger = logger;
        }

        public async Task<List<AppointmentDAO>> sp_ListAppointmentRepository()
        {
            try
            {

                List<AppointmentDAO> appointmentDAOs = new List<AppointmentDAO>();
                using (var command = _unitOfWork._context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "sp_ListAppointment";
                    command.CommandType = CommandType.StoredProcedure;
                    _unitOfWork._context.Database.OpenConnection();

                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Rows)
                    {
                        AppointmentDAO appointmentDAO = new AppointmentDAO();
                        appointmentDAO.Appointment_Id = Convert.ToInt32(item.ItemArray[0].ToString());
                        appointmentDAO.PatientId = Convert.ToInt32(item.ItemArray[1].ToString());
                        appointmentDAO.DoctorId = Convert.ToInt32(item.ItemArray[2].ToString());
                        appointmentDAO.Appointment_StartUtc = string.IsNullOrEmpty(item.ItemArray[3].ToString()) ? null : Convert.ToDateTime(item.ItemArray[3].ToString());
                        appointmentDAO.Appointment_EndUtc = string.IsNullOrEmpty(item.ItemArray[4].ToString()) ? null : Convert.ToDateTime(item.ItemArray[4].ToString());
                        appointmentDAO.Appointment_Diagnosis = item.ItemArray[5].ToString();
                        appointmentDAO.Appointment_Room = item.ItemArray[6].ToString();
                        appointmentDAO.Appointment_Status = item.ItemArray[7].ToString();
                        appointmentDAO.Appointment_CreatedBy = item.ItemArray[8].ToString();
                        appointmentDAO.Appointment_CreatedAt = string.IsNullOrEmpty(item.ItemArray[9].ToString()) ? null : Convert.ToDateTime(item.ItemArray[9].ToString());
                        appointmentDAO.Appointment_ModifiedBy = item.ItemArray[10].ToString();
                        appointmentDAO.Appointment_ModifiedAt = string.IsNullOrEmpty(item.ItemArray[11].ToString()) ? null : Convert.ToDateTime(item.ItemArray[11].ToString());
                        appointmentDAOs.Add(appointmentDAO);
                    }
                }
                return appointmentDAOs;

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
