using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace KasherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private IConfiguration _configuration;

        public employeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllEmployee")]
        public JsonResult GetDepartment()
        {
            try
            {
                string query = "select employeeID from dbo.employee";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        myReader = sqlCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        sqlConnection.Close();
                    }
                }
                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public JsonResult DeleteUsers(int ID)
        {
            string query = "delete from dbo.employee where employeeID=@employeeID";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@employeeID", ID);
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
