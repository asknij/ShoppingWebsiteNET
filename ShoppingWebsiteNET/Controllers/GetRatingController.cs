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
    public class GetRatingController : ControllerBase
    {



        private readonly IConfiguration _configuration;
        public GetRatingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult Post(Comments c)
        {
            string query = "SELECT AVG(rating) as average FROM comments where product=@product";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ProductAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    myCommand.Parameters.AddWithValue("@product", c.product);
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
