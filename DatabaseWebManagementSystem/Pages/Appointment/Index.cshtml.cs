using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DatabaseWebInterface.Pages.Appointment
{

    public class IndexModel : PageModel
    {

        public List<AppointmentInfo> listAppointment = new List<AppointmentInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-07NFG6R;Initial Catalog=PersonalCareDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Appointment";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AppointmentInfo appointmentInfo = new AppointmentInfo();
                                appointmentInfo.AppointmentID = reader.GetInt32(0).ToString();
                                appointmentInfo.AppointmentDate = reader.GetDateTime(1);
                                appointmentInfo.AppointmentHour = reader.GetTimeSpan(2);

                                appointmentInfo.CustomerID = reader.GetInt32(3).ToString();
                                appointmentInfo.ServiceID = reader.GetInt32(4).ToString();


                                listAppointment.Add(appointmentInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public class AppointmentInfo
    {
        public String AppointmentID;
        public DateTime AppointmentDate;
        public TimeSpan AppointmentHour;
        public String CustomerID;
        public String ServiceID;
    }
}
