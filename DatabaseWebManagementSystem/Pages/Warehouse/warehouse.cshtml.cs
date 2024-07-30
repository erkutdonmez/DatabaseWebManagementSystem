using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Identity.Data;


namespace DatabaseWebInterface.Pages.Warehouse
{
    public class warehouseModel : PageModel
    {
        public List<WarehouseInfo> listWarehouse = new List<WarehouseInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-07NFG6R;Initial Catalog=PersonalCareDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * From Warehouse ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WarehouseInfo info = new WarehouseInfo();
                                info.WarehouseCity = reader.GetString(0);
                                info.WarehouseDistrict = reader.GetString(1);
                                info.WarehouseNeighbourhood = reader.GetString(2);
                                info.WarehouseStreet = reader.GetString(3);
                                info.WarehouseBuildingNumber = reader.GetInt32(4); 
                                info.WarehouseApartmentNumber = reader.GetInt32(5);

                                listWarehouse.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class WarehouseInfo
    {
        public String WarehouseCity;
        public String WarehouseDistrict;
        public String WarehouseNeighbourhood;
        public String WarehouseStreet;
        public int WarehouseBuildingNumber;
        public int WarehouseApartmentNumber;
    }

}

