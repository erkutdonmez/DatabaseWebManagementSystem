using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace DatabaseWebInterface.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            customerInfo.CustomerName = Request.Form["name"];
            customerInfo.CustomerSurname = Request.Form["surname"];
            customerInfo.CustomerAddress = Request.Form["address"];
            customerInfo.CustomerBudget = Request.Form["budget"];
            customerInfo.ContactNumber = Request.Form["number"];
            if(customerInfo.CustomerName.Length == 0 ||
                customerInfo.CustomerSurname.Length == 0 || customerInfo.CustomerAddress.Length==0 ||
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
                    String sql = 
                        "insert into Customer (CustomerName,CustomerSurname,CustomerAddress,CustomerBudget,ContactNumber)" +
                        "values (@name,@surname,@address,@budget,@number);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.CustomerName);
                        command.Parameters.AddWithValue("@surname", customerInfo.CustomerSurname);
                        command.Parameters.AddWithValue("@address", customerInfo.CustomerAddress);
                        command.Parameters.AddWithValue("@budget", customerInfo.CustomerBudget);
                        command.Parameters.AddWithValue("@number", customerInfo.ContactNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            customerInfo.CustomerName = "";
            customerInfo.CustomerSurname = "";
            customerInfo.CustomerAddress = "";
            customerInfo.CustomerBudget = "";
            customerInfo.ContactNumber = "";
            successMessage = "New customer has been added properly.";
        }    
    }
}
