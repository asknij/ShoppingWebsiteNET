using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ShoppingWebsiteNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = "select * from product";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ProductAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn=new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand=new SqlCommand(query,myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
