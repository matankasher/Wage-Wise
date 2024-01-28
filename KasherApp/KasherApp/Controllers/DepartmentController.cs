using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace KasherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetDepartment")]
        public JsonResult GetDepartment()
        {
            try
            {
                string query = "select name from dbo.department";
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
        [HttpGet]
        [Route("GetWageDepartment")]
        public JsonResult GetWageDepartment(string name)
        {
            try
            {
                string query = "select wage from dbo.department where name= @name";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    { 
                        sqlCommand.Parameters.AddWithValue("@name", name);
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
        [HttpGet]
        [Route("GetEmployeeDepartment")]
        public JsonResult GetEmployeeDepartment(int id)
        {
            try
            {
                string query = "select department from dbo.employee where employeeID = @ID";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@ID", id);
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
        [Route("AddDepartment")]
        public JsonResult AddDepartment([FromForm] string department , [FromForm] int wage)
        {
            string query = "insert into dbo.department values (@department, @wage)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@department", department);
                    sqlCommand.Parameters.AddWithValue("@wage", wage);
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        [HttpPost]
        [Route("UpdateDepartment")]
        public JsonResult UpadteDepartment([FromForm] string department, [FromForm] int wage)
        {
            string query = "update dbo.department SET wage = @wage WHERE name = @department";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@wage", wage);
                    sqlCommand.Parameters.AddWithValue("@department", department);
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return new JsonResult("Updated Successfully");
                    }
                    else
                    {
                        return new JsonResult("No rows were updated.");
                    }
                }
            }
        }
        [HttpDelete]
        [Route("DeleteDepartment")]
        public JsonResult DeleteUsers(string department)
        {
            string query = "delete from dbo.Department where name=@department";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("kasherDB");
            SqlDataReader myReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Department", department);
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
