using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace KasherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private IConfiguration _configuration;

        public loginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPassword")]
        public JsonResult GetUsers(string ID)
        {
            try
            {
                string query = "select password from dbo.employee where employeeID = @ID";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@ID", ID);
                        myReader = sqlCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        sqlConnection.Close();
                    }
                }
                return new JsonResult(table);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }
        }
        [HttpGet]
        [Route("GetName")]
        public JsonResult GetID(string ID)
        {
            try
            {
                string query = "select FirstName,lastName from dbo.employee where employeeID = @ID";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@ID", ID);
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

        [HttpPost]
        [Route("AddUser")]
        public JsonResult AddUsers([FromForm] int ID ,[FromForm] string password, [FromForm] string firstName, [FromForm] string lastName, [FromForm] string Department )
        {
            string query = "insert into dbo.employee values (@employeeID , @password, @firstName, @LastName ,  @department)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@employeeID",   ID);
                    sqlCommand.Parameters.AddWithValue("@password", password);
                    sqlCommand.Parameters.AddWithValue("@firstName", firstName);
                    sqlCommand.Parameters.AddWithValue("@lastName", lastName);
                    sqlCommand.Parameters.AddWithValue("@department", Department);
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
    }
}
