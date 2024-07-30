using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DatabaseWebInterface.Pages.MyOrder
{
    public class IndexModel : PageModel
    {

        public List<MyOrderInfo> listMyOrder = new List<MyOrderInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-07NFG6R;Initial Catalog=PersonalCareDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MyOrder";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MyOrderInfo myOrderInfo = new MyOrderInfo();
                                myOrderInfo.OrderID = "" + reader.GetInt32(0);
                                myOrderInfo.OrderDate = reader.GetDateTime(1);
                                myOrderInfo.EmployeeID = ""+reader.GetInt32(2);
                                myOrderInfo.CustomerID = "" + reader.GetInt32(3);
                                myOrderInfo.ProductID = "" + reader.GetInt32(4);

                                listMyOrder.Add(myOrderInfo);
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
    public class MyOrderInfo
    {
        public String OrderID;
        public DateTime OrderDate;
        public String EmployeeID;
        public String CustomerID;
        public String ProductID;
    }
}
