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
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CommentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "select * from comments";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ProductAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Comments c)
        {
            string query = @"INSERT INTO comments([user], product, comment, rating) VALUES (@user, @product, @comment, @rating)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ProductAppCon");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDatasource))
                {
                    myConn.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myConn))
                    {
                        myCommand.Parameters.AddWithValue("@product", c.product);
                        myCommand.Parameters.AddWithValue("@user", c.user);
                        myCommand.Parameters.AddWithValue("@rating", c.rating);
                        myCommand.Parameters.AddWithValue("@comment", c.comment);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConn.Close();
                    }
                }
                return new JsonResult("Added Sucessfully");
            }
            catch
            {
                return new JsonResult("Error While Adding");
            }
        }

    }

}
