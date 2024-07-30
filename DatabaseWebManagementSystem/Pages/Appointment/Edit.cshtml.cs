using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DatabaseWebInterface.Pages.Appointment
{
    public class EditModel : PageModel
    {
        public AppointmentInfo appointmentInfo = new AppointmentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-07NFG6R;Initial Catalog=PersonalCareDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Appointment where AppointmentID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                //appointmentInfo.AppointmentID = reader.GetInt32(0).ToString();
                                appointmentInfo.AppointmentDate = reader.GetDateTime(1);
                                appointmentInfo.AppointmentHour = reader.GetTimeSpan(2);
                                appointmentInfo.CustomerID = reader.GetInt32(3).ToString();
                                appointmentInfo.ServiceID = reader.GetInt32(4).ToString();

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

            }
        }
        public void OnPost()
        {
      
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
                    String sql = "update Appointment " +
                        "SET AppointmentDate = @date, AppointmentHour = @hour, CustomerID = @cid, ServiceID = @sid" +
                        " where AppointmentID  = @id ";
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
            appointmentInfo.AppointmentDate = DateTime.MinValue;


            Response.Redirect("/Appointment/Index");
        }
    }
}
