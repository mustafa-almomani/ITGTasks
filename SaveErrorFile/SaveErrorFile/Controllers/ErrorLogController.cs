using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveErrorFile.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace SaveErrorFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        private readonly MyDbContext _context;

  
        public ErrorLogController(MyDbContext context)
        {
            _context = context;
        }

        #region SaveError Method

        /// <summary>
        /// This method saves the error log to the database.
        /// It expects an `ErrorLog` object in the request body and adds it to the database.
        /// If the provided log is invalid, it returns a `BadRequest`.
        /// </summary>
        /// <param name="log">The error log to save.</param>
        /// <returns>An action result indicating the result of the operation.</returns>
        [HttpPost("Save")]
        public async Task<IActionResult> SaveError([FromBody] ErrorLog log)
        {
            if (log == null || string.IsNullOrEmpty(log.Message))
                return BadRequest("Invalid log data.");

            try
            {
                var parameters = new[]
                {
            new SqlParameter("@Message", log.Message),
            new SqlParameter("@StackTrace", log.StackTrace),
            new SqlParameter("@Source", log.Source)
             };

                var result = await _context.Database.ExecuteSqlRawAsync("EXEC SaveErrorLog @Message, @StackTrace, @Source", parameters);

                return Ok(new { message = "Error logged successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }

        #endregion

        #region GetAllErrorLogs Method

        /// <summary>
        /// This method retrieves all error logs from the database.

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllErrorLogs()
        {
            try
            {
                var logs = await _context.ErrorLogs.FromSqlRaw("EXEC getAllError").ToListAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }
        #endregion
    }
}

