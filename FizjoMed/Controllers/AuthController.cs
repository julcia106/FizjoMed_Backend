using FizjoMed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FizjoMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //user login
        [HttpPut("Login")]
        public JsonResult Put(Admin admin)
        {
            string sqlDataSource = _configuration.GetConnectionString("FizjoMedCon");

            // conn and reader declared outside try
            // block for visibility in finally block
            SqlConnection con = null;
            SqlDataReader reader = null;
            bool isAdmin = false;

            try
            {
                con = new SqlConnection(sqlDataSource);
                con.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand(
                    "select * from Administrator where LoginName = @LoginName AND AdminPassword = @AdminPassword", con);

                // 2. define and add parameters used in command object
                cmd.Parameters.Add("@LoginName", SqlDbType.VarChar, 50).Value = admin.LoginName;
                cmd.Parameters.Add("@AdminPassword", SqlDbType.VarChar, 50).Value = admin.AdminPassword;

                // get data stream
                reader = cmd.ExecuteReader();

                // write record
                if (reader.HasRows)
                {
                    isAdmin = true;
                    string output = isAdmin ? "Welcome to the Admin User" : "Welcome to the User";
                    return new JsonResult(isAdmin);

                }
                else
                {
                    return new JsonResult(isAdmin);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception: ", ex);
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}
