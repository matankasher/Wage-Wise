using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace KasherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class managerController : ControllerBase
    {
        private IConfiguration _configuration;

        public managerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPassword")]
        public JsonResult GetUsers(string ID)
        {
            DataTable table = new DataTable();
            DataColumn column = new DataColumn();
            ManagerAdmin manager = ManagerAdmin.getInstance();
            
            column.ColumnName = "password";
            table.Columns.Add(column);

            DataRow row = table.NewRow();
            if(ID.Equals(manager.Id))
                row["password"] = manager.Password;
            table.Rows.Add(row);
            return new JsonResult(table);
        }
    }
}
