using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TEST.classes;
using System.Xml;
using System.IO;

namespace TEST
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string defaultLanguage = "en";

            if (HttpContext.Current.Session != null && Session["PreferredLanguage"] != null)
            {
                defaultLanguage = Session["PreferredLanguage"].ToString();
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(defaultLanguage);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(defaultLanguage);
        }


        #region Application_Error Method

        /// <summary>
        /// This method is triggered when an unhandled error occurs in the application.
        /// It captures the exception details and logs them asynchronously to an API and an XML file.
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            string sMessage = exception.InnerException?.Message ?? exception.Message;
            string sStackTrace = exception.InnerException?.StackTrace ?? exception.StackTrace;
            string sSource = exception.InnerException?.Source ?? exception.Source;

            ErrorLog errorLog = new ErrorLog();
            Task.Run(() => errorLog.LogErrorAsync(sMessage, sStackTrace, sSource));

            string filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ErrorLogFilePath"]);

            LogErrorToXml(filePath, sMessage, sStackTrace, sSource);
        }

        #endregion


        #region LogErrorToXml Method

        /// <summary>
        /// This method logs the error details into an XML file.
        /// If the file exists, it appends the error. If not, it creates a new file.
        /// </summary>
        /// <param name="filePath">The file path where the XML error log will be saved.</param>
        /// <param name="message">The error message.</param>
        /// <param name="stackTrace">The stack trace of the error.</param>
        /// <param name="source">The source of the error.</param>
        private void LogErrorToXml(string sFilePath, string sMessage, string sStackTrace, string sSource)
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(sFilePath))
            {
                xmlDoc.Load(sFilePath);
            }
            else
            {
                XmlNode rootNode = xmlDoc.CreateElement("ErrorLog");
                xmlDoc.AppendChild(rootNode);
            }

            XmlNode errorNode = xmlDoc.CreateElement("Error");

            XmlNode errorTimeNode = xmlDoc.CreateElement("ErrorTime");
            errorTimeNode.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            errorNode.AppendChild(errorTimeNode);
             
            XmlNode tracingNode = xmlDoc.CreateElement("Tracing");
            tracingNode.InnerText = sStackTrace;
            errorNode.AppendChild(tracingNode);

            XmlNode errorMessageNode = xmlDoc.CreateElement("ErrorMessage");
            errorMessageNode.InnerText = sMessage;
            errorNode.AppendChild(errorMessageNode);

            xmlDoc.DocumentElement.AppendChild(errorNode);

            xmlDoc.Save(sFilePath);
        }

        #endregion
    

}
}