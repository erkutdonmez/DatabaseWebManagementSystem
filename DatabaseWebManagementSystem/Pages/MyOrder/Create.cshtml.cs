using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace DatabaseWebInterface.Pages.MyOrder
{
    public class CreateModel : PageModel
    {
        public MyOrderInfo myOrderInfo = new MyOrderInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //myOrderInfo.OrderID = Request.Form["id"];
            myOrderInfo.OrderDate = DateTime.Parse(Request.Form["date"]);
            myOrderInfo.EmployeeID = Request.Form["eid"];
            myOrderInfo.CustomerID = Request.Form["cid"];
            myOrderInfo.ProductID = Request.Form["pid"];
            if (myOrderInfo.EmployeeID.Length == 0 ||
                myOrderInfo.CustomerID.Length == 0 || myOrderInfo.ProductID.Length == 0 
                )
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
                        "insert into MyOrder (OrderDate,EmployeeID,CustomerID,ProductID) " +
                    "values (@date,@eid,@cid,@pid)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@date", myOrderInfo.OrderDate);
                        command.Parameters.AddWithValue("@eid", myOrderInfo.EmployeeID);
                        command.Parameters.AddWithValue("@cid", myOrderInfo.CustomerID);
                        command.Parameters.AddWithValue("@pid", myOrderInfo.ProductID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            myOrderInfo.OrderDate = DateTime.MinValue;
            myOrderInfo.EmployeeID = "";
            myOrderInfo.CustomerID = "";
            myOrderInfo.ProductID = "";
            successMessage = "New order has been added properly.";
        }
    }
}
