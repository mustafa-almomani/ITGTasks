using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;

namespace TEST
{
    public partial class LOGIN : System.Web.UI.Page
    {
        #region Page Events

        /// <summary>
        /// Handles the Page Load event. Sets localized text for UI elements on the first load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                title.InnerText = GetLocalResourceObject("title").ToString();
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

        #region Button Events

        /// <summary>
        /// Handles the login button click event. Authenticates the user and redirects based on user type.
        /// Displays an error message if authentication fails.
        /// </summary>
        /// <param name="sender">The source of the button click.</param>
        /// <param name="e">Event data.</param>

        protected void btnLogin(object sender, EventArgs e)
        {
            try
            {
                Login userManager = new Login();
                User loggedInUser = userManager.AuthenticateUser(tbusserName.Text.Trim(), tbpassword.Text);

                if (loggedInUser != null)
                {
                    Session["LoggedInUser"] = loggedInUser;

                    if (loggedInUser.UserType == UserType.User)
                    {
                        Response.Redirect("profile.aspx");
                    }
                    else
                    {
                        Response.Redirect("task1.aspx");
                    }
                }
                else
                {
                    lblError.Text = "Invalid username or password!";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }
        #endregion
    }
}
