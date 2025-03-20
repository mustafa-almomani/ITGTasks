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
    public partial class profile : System.Web.UI.Page
    {

        #region Load User Data
        /// <summary>
        /// Handles the Page Load event. On first load, checks if a user is logged in via session.
        /// If a logged-in user exists, sets the UserId for the UserDetailsControl and loads the user’s data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

           
            if (!IsPostBack)
            {
                if (Session["LoggedInUser"] != null)
                {
                    User user = (User)Session["LoggedInUser"];
                   

                    //int userId =Convert.ToInt32(Session["Userid"]) ;

                    UserDetailsControl.UserId = user.Id;

                    UserDetailsControl.LoadUserData();

                }
            }


        }
        #endregion



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