
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace TEST
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ImageService : WebService
    {
        //private static readonly string logFilePath = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");

        [WebMethod]
        public string UploadImage(string imageBytes, string fileName)
        {
            string uploadFolder = ConfigurationManager.AppSettings["ImageUploadPath"];
            try
            {
                if (string.IsNullOrEmpty(uploadFolder))
                {
                    return "❌ Error: Upload path is not configured.";
                }

                string physicalPath = uploadFolder;

                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                string extension = Path.GetExtension(fileName).ToLower();
                List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    return "❌ Error: Invalid file format. Only JPG , JPEG and PNG are allowed.";
                }

                byte[] imageData = Convert.FromBase64String(imageBytes);
                string filePath = Path.Combine(physicalPath, fileName);
                File.WriteAllBytes(filePath, imageData);

                return filePath;
            }
            catch (Exception ex)
            {
                return "❌ Error: " + ex.Message;
            }
        }


        [WebMethod]
        public string GetImage(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return "❌ Error: No file name provided.";
                }

                string filePath = Path.Combine(ConfigurationManager.AppSettings["ImageUploadPath"], fileName);
                string baseImageUrl = ConfigurationManager.AppSettings["BaseImageUrl"];

                if (!File.Exists(filePath))
                {
                    return "❌ Error: File not found.";
                }

                // ✅ إرجاع الرابط الصحيح للصورة
                //string imageUrl = $"http://itg-mmmomani/ExternalImages/{fileName}";

                string imageUrl = $"{baseImageUrl}/{fileName}";

                return imageUrl;
            }
            catch (Exception ex)
            {
                return "❌ Error: " + ex.Message;
            }
        }


    }
}