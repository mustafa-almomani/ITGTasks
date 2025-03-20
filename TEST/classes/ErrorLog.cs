using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace TEST.classes
{
    public class ErrorLog
    {
       private readonly  string sApi = ConfigurationManager.AppSettings["ApiSaveErroreFile"];

        #region LogErrorAsync Method

        /// <summary>
        /// Asynchronously logs an error to the remote API.
        /// This method sends the error details (message, stack trace, source, and logged time)
        /// to the API defined by the _apiUrl field.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="stackTrace">The stack trace of the error.</param>
        /// <param name="source">The source of the error (where the error occurred).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LogErrorAsync(string sMessage, string SstackTrace, string Ssource)
        {
            var errorLog = new
            {
                Message = sMessage,
                StackTrace = SstackTrace,
                Source = Ssource,
                LoggedAt = DateTime.Now
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(JsonConvert.SerializeObject(errorLog), Encoding.UTF8, "application/json");
                await client.PostAsync(sApi, content);
            }

        }
        #endregion
    }
}