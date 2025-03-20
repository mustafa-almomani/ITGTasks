using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TEST
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region Culture Initialization

        /// <summary>
        /// Initializes the culture settings for the page based on the user's preferred language stored in session.
        /// </summary>
        protected override void InitializeCulture()
        {
            string preferredLanguage = "en";

            if (Session["PreferredLanguage"] != null)
            {
                preferredLanguage = Session["PreferredLanguage"].ToString();
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(preferredLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(preferredLanguage);

            base.InitializeCulture();
        }
        #endregion
    }

}