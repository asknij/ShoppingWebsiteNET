using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingWebsiteNET.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingWebsiteNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        [HttpPost]
        public JsonResult Post(user u )
        {
            string query = @"SELECT * from [user] where username=@username and password=@password";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ProductAppCon");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@username", u.username);
                    myCommand.Parameters.AddWithValue("@password", u.password);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
                if (table.Rows.Count> 0)
                {
                    return new JsonResult("True");
                }
                else
                    return new JsonResult("False");
            }
    
        


    }
}
