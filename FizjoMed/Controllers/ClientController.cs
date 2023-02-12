using FizjoMed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FizjoMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Id, FirstName, LastName, EmailAdress, PhoneNumber, PersonalPassword, LoginName,
                            DateOfJoining
                            from 
                            dbo.Client
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FizjoMedCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Client client)
        {
            string query = @"
                            insert into dbo.Client
                            (FirstName, LastName,EmailAdress,PhoneNumber, PersonalPassword, LoginName, DateOfJoining)
                           values (@FirstName, @LastName, @EmailAdress, @PhoneNumber, @PersonalPassword, @LoginName, @DateOfJoining)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FizjoMedCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", client.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", client.LastName);
                    myCommand.Parameters.AddWithValue("@EmailAdress", client.EmailAdress);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@PersonalPassword", client.PersonalPassword);
                    myCommand.Parameters.AddWithValue("@LoginName", client.LoginName);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", client.DateOfJoining);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Client client)
        {
            string query = @"
                            update dbo.Client 
                            set FirstName= @FirstName,
                            LastName = @LastName,
                            EmailAdress = @EmailAdress,
                            PhoneNumber = @PhoneNumber,
                            PersonalPassword = @PersonalPassword,
                            LoginName = @LoginName,
                            DateOfJoining = @DateOfJoining
                            where Id = @Id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FizjoMedCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", client.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", client.LastName);
                    myCommand.Parameters.AddWithValue("@EmailAdress", client.EmailAdress);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@PersonalPassword", client.PersonalPassword);
                    myCommand.Parameters.AddWithValue("@LoginName", client.LoginName);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", client.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@Id", client.Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Client 
                            where Id = @Id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FizjoMedCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
