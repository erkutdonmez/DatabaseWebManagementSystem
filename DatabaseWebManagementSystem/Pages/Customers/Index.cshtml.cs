using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DatabaseWebInterface.Pages.Customers
{
    public class IndexModel : PageModel
    {
        
        public List<CustomerInfo> listCustomers = new List<CustomerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-07NFG6R;Initial Catalog=PersonalCareDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Customer";
                    using(SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerInfo customerInfo = new CustomerInfo();
                                customerInfo.CustomerID = "" + reader.GetInt32(0);
                                customerInfo.CustomerName = reader.GetString(1);
                                customerInfo.CustomerSurname = reader.GetString(2);
                                customerInfo.CustomerAddress = reader.GetString(3);
                                customerInfo.CustomerBudget = "" + reader.GetInt32(4);
                                customerInfo.ContactNumber = reader.GetString(5);

                                listCustomers.Add(customerInfo);
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
    public class CustomerInfo
    {
        public String CustomerID;
        public String CustomerName;
        public String CustomerSurname;
        public String CustomerAddress;
        public String CustomerBudget;
        public String ContactNumber;
    }
}
