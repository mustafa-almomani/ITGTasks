using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ImageController : ApiController
    {
        private readonly string _imagePath;

        public ImageController()
        {
            _imagePath = ConfigurationManager.AppSettings["ImageUploadPath"] ;
        }

        [HttpDelete]
        [Route("api/Image/DeleteImage/{fileName}")]
        public IHttpActionResult DeleteImage(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return BadRequest("❌ Error: File name is required.");
                }

                string filePath = Path.Combine(_imagePath, fileName);

                if (!File.Exists(filePath))
                {
                    return NotFound();
                }

                File.Delete(filePath);

                return Ok($"✅ Image '{fileName}' deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
