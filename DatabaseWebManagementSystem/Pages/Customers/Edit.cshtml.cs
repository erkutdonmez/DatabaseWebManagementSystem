using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DatabaseWebInterface.Pages.Customers
{
    public class EditModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
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
                    String sql = "SELECT * FROM Customer where CustomerID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customerInfo.CustomerID = "" + reader.GetInt32(0);
                                customerInfo.CustomerName = reader.GetString(1);
                                customerInfo.CustomerSurname = reader.GetString(2);
                                customerInfo.CustomerAddress = reader.GetString(3);
                                customerInfo.CustomerBudget = "" + reader.GetInt32(4);
                                customerInfo.ContactNumber = reader.GetString(5);

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
            customerInfo.CustomerID = Request.Form["id"];
            customerInfo.CustomerName = Request.Form["name"];
            customerInfo.CustomerSurname = Request.Form["surname"];
            customerInfo.CustomerAddress = Request.Form["address"];
            customerInfo.CustomerBudget = Request.Form["budget"];
            customerInfo.ContactNumber = Request.Form["number"];

            if (customerInfo.CustomerID.Length == 0 || customerInfo.CustomerName.Length == 0 ||
                customerInfo.CustomerSurname.Length == 0 || customerInfo.CustomerAddress.Length == 0 ||
                customerInfo.CustomerBudget.Length == 0 || customerInfo.ContactNumber.Length == 0)
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
                    String sql = "update Customer " +
                        "SET CustomerName = @name, CustomerSurname = @surname, CustomerAddress = @address, CustomerBudget = @budget, ContactNumber = @number" +
                        " where CustomerID  = @id ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", customerInfo.CustomerID);
                        command.Parameters.AddWithValue("@name", customerInfo.CustomerName);
                        command.Parameters.AddWithValue("@surname", customerInfo.CustomerSurname);
                        command.Parameters.AddWithValue("@address", customerInfo.CustomerAddress);
                        command.Parameters.AddWithValue("@budget", customerInfo.CustomerBudget);
                        command.Parameters.AddWithValue("@number", customerInfo.ContactNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Customers/Index");
        }
    }
}
