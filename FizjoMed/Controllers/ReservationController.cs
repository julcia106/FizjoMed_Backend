using FizjoMed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FizjoMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public ReservationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getAvailableReservations")]
        public JsonResult GetAvailableReservations() //rezerwacja zabiegu
        {
            string query = @"
                            SELECT 
	                            t.TreatmentName,
	                            worker_id,
                                reservation_id,
                                treatment_id,
	                            LEFT(CONVERT(varchar,startTime, 120), 10) AS startDate,
	                            LEFT(CONVERT(varchar,endTime, 120), 10) AS endDate,
	                            LEFT(CONVERT(varchar,startTime,108), 5) As startTime,
	                            LEFT(CONVERT(varchar,endTime,108), 5) As endTime
                            FROM dbo.Reservation 
	                            join Treatments as t ON Reservation.treatment_id= t.TreatmentId
	                            join Worker as w ON Reservation.worker_id = w.WorkerId
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

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT 
	                            t.TreatmentName,
	                            client_Name,
	                            client_Surname,
	                            client_Phone,
	                            client_Email,
	                            LEFT(CONVERT(varchar,startTime, 120), 10) AS startDate,
	                            LEFT(CONVERT(varchar,endTime, 120), 10) AS endDate,
	                            LEFT(CONVERT(varchar,startTime,108), 5) As startTime,
	                            LEFT(CONVERT(varchar,endTime,108), 5) As endTime
                            FROM dbo.Reservation 
                            join Treatments as t ON Reservation.treatment_id= t.TreatmentId
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

        [HttpPut]
        public JsonResult CreateReservation(Reservation reservation)
        {
            string query = @"
                            update dbo.Reservation 
                            set 
                                treatment_id = @treatment_id,
                                client_Name = @client_Name,
                                client_Surname = @client_Surname,
                                client_Phone = @client_Phone,
                                client_Email = @client_Email,
                                worker_id = @worker_id
                            where reservation_id = @reservation_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FizjoMedCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@treatment_id", reservation.treatment_id);
                    myCommand.Parameters.AddWithValue("@worker_id", reservation.worker_id);
                    myCommand.Parameters.AddWithValue("@client_Name", reservation.client_Name);
                    myCommand.Parameters.AddWithValue("@client_Surname", reservation.client_Surname);
                    myCommand.Parameters.AddWithValue("@client_Phone", reservation.client_Phone);
                    myCommand.Parameters.AddWithValue("@client_Email", reservation.client_Email);
                    myCommand.Parameters.AddWithValue("@reservation_id", reservation.reservation_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
    }
}
