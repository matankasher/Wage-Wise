using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace KasherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class shiftsController : ControllerBase
    {
        private IConfiguration _configuration;

        public shiftsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddShift")]
        public JsonResult AddShift([FromForm] int employeeID, [FromForm] DateTime shiftDate, [FromForm] double hours)
        {
            try
            {
                string query = "insert into dbo.shifts values (@employeeID , @shiftDate, @hours)";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@employeeID", employeeID);
                        sqlCommand.Parameters.AddWithValue("@shiftDate", shiftDate);
                        sqlCommand.Parameters.AddWithValue("@hours", hours);
                        myReader = sqlCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        sqlConnection.Close();
                    }
                }
                return new JsonResult("Added Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.ToString());
            }
        }
        [HttpGet]
        [Route("GetNumShiftInMonth")]
        public JsonResult GetNumShiftInMonth( int employeeID, int month, int year)
        {
            try
            {
                string query = "select count(hours) from dbo.shifts Where MONTH(shiftDate) = @month AND YEAR(shiftDate) = @year AND employeeID = @employeeID";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@month", month);
                        sqlCommand.Parameters.AddWithValue("@employeeID", employeeID);
                        sqlCommand.Parameters.AddWithValue("@year", year);
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
        [Route("GetNumHoursInMonth")]
        public JsonResult GetNumHoursInMonth(int employeeID, int month, int year)
        {
            try
            {
                string query = "select sum(hours) from dbo.shifts Where MONTH(shiftDate) = @month AND YEAR(shiftDate) = @year AND employeeID = @employeeID";
                DataTable table = new DataTable();
                string sqlDatasource = _configuration.GetConnectionString("kasherDB");
                SqlDataReader myReader;
                using (SqlConnection sqlConnection = new SqlConnection(sqlDatasource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@month", month);
                        sqlCommand.Parameters.AddWithValue("@employeeID", employeeID);
                        sqlCommand.Parameters.AddWithValue("@year", year);
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
    }

    }
