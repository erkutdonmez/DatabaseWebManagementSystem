using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace DatabaseWebInterface.Pages.Appointment
{
    public class CreateModel : PageModel
    {
        public AppointmentInfo appointmentInfo = new AppointmentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //appointmentInfo.AppointmentID = Request.Form["id"];
            appointmentInfo.AppointmentDate = DateTime.Parse(Request.Form["date"]);
            appointmentInfo.AppointmentHour = TimeSpan.Parse(Request.Form["hour"]);
            appointmentInfo.CustomerID = Request.Form["cid"];
            appointmentInfo.ServiceID = Request.Form["sid"];
            if (
                appointmentInfo.CustomerID.Length == 0 || appointmentInfo.ServiceID.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }
            try
            {
                String connectionString = "Data Source=DESKTOP-07NFG6R;Initial Catalog=PersonalCareDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = 
                        "insert into Appointment (AppointmentDate,AppointmentHour,CustomerID,ServiceID)" +
                        "values (@date,@hour,@cid,@sid);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@date", appointmentInfo.AppointmentDate);
                        command.Parameters.AddWithValue("@hour", appointmentInfo.AppointmentHour);
                        command.Parameters.AddWithValue("@cid", appointmentInfo.CustomerID);
                        command.Parameters.AddWithValue("@sid", appointmentInfo.ServiceID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            //appointmentInfo.AppointmentID = "";
            appointmentInfo.AppointmentDate = DateTime.MinValue;
            appointmentInfo.AppointmentHour = TimeSpan.Zero;
            appointmentInfo.CustomerID = "";
            appointmentInfo.ServiceID = "";
            successMessage = "New appointment has been added properly.";
        }
    }
}
