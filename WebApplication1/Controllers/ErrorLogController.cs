using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Classes;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/ErrorLog")]

    public class ErrorLogController : ApiController
    {
       
    private readonly string _connString = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;

        [HttpPost]
        [Route("Save")]
        public IHttpActionResult SaveError(ErrorLog log)
        {
            if (log == null || string.IsNullOrEmpty(log.Message))
                return BadRequest("Invalid log data.");

            try
            {
                int insertedId = 0;

                using (SqlConnection conn = new SqlConnection(_connString))
                using (SqlCommand cmd = new SqlCommand("SaveErrorLog", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Message", log.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", (object)log.StackTrace ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Source", (object)log.Source ?? DBNull.Value);

                    conn.Open();
                    insertedId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                return Ok(new { message = "Error logged.", id = insertedId });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Internal Error: " + ex.Message));
            }
        }

        // GET: api/ErrorLogs/GetAll
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllErrorLogs()
        {
            var logs = new List<ErrorLog>();

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand("GetAllErrorLogs", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    logs.Add(new ErrorLog
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Message = reader["Message"].ToString(),
                        StackTrace = reader["StackTrace"].ToString(),
                        Source = reader["Source"].ToString(),
                        LoggedAt = Convert.ToDateTime(reader["LoggedAt"])
                    });
                }
            }

            return Ok(logs);
        }
    }
}
