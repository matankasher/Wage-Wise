using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace KasherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private IConfiguration _configuration;

        public usersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("GetUsers")]
        public JsonResult GetUsers()
        {
            string query = "select * from dbo.users";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection)) 
                { 
                    myReader=sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult(table); 
        }

        [HttpPost]
        [Route("AddUsers")]
        public JsonResult AddUsers([FromForm] string firstName, [FromForm] string lastName )
        {
            string query = "insert into dbo.users values (@first , @last)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@first", firstName);
                    sqlCommand.Parameters.AddWithValue("@last", lastName);
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }


        [HttpDelete]
        [Route("DeleteUsers")]
        public JsonResult DeleteUsers(string firstName, string lastName)
        {
            string query = "delete from dbo.users where firstName=@first AND lastName=@last";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@first", firstName);
                    sqlCommand.Parameters.AddWithValue("@last", lastName);
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
