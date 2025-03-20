using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TEST
{
    public partial class UserDetails : System.Web.UI.UserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ID of the user whose details will be displayed.
        /// </summary>
        public int UserId { get; set; }

        #endregion

        #region Page Events

        /// <summary>
        /// Handles the Page Load event. Redirects to login if no user is in session.
        /// Sets the page title and handles visibility of the back button based on query string.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title.Text = GetLocalResourceObject("Title")?.ToString();
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            //  LoadUserData(UserId);
            string userId = Request.QueryString["userId"];

            // Check if user is of type "User" and if userId exists in the query string
            if ( string.IsNullOrEmpty(userId))
            {
                btnBack1.Visible = false; // Hide the button if user is User type and userId is not in the URL
            }
            else
            {
                btnBack1.Visible = true;
            }


        }
        #endregion


        #region Public Methods

        /// <summary>
        /// Loads user data from the database based on UserId and binds the information to UI elements.
        /// Displays user details such as name, email, client name, birthday, gender, language, and profile image.
        /// </summary>
        public void LoadUserData()
        {
            string baseImageUrl = ConfigurationManager.AppSettings["BaseImageUrl"];

            User user = TASK1.GetUserById(UserId);
            if (UserId != null)
            {
              
                lblUserName.Text = GetLocalResourceObject("TemplateFieldResource1")?.ToString() + " " + user.UserName;
                lblEmail.Text = GetLocalResourceObject("lblEmail")?.ToString() + " " + user.Email;
                lblClientName.Text = GetLocalResourceObject("lblclientName")?.ToString() + " " + user.ClientName;
                lblBirthday.Text = GetLocalResourceObject("lblBirthDay")?.ToString() + " " + Convert.ToDateTime(user.Birthday)
                        .ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                lblGender.Text = GetLocalResourceObject("lblgender")?.ToString() + " " + user.Gender;
                lblLanguage.Text = GetLocalResourceObject("lbllanguage")?.ToString() + " " + user.LanguageName;
                lbNational_ID.Text = GetLocalResourceObject("lbNational_ID")?.ToString() + " " + user.National_ID;
                //imgUser.ImageUrl = !string.IsNullOrEmpty(user.IMG)
                // ? $"http://itg-mmmomani/ExternalImages/{user.IMG}"
                // : "~/Images/default.png";
                imgUser.ImageUrl = !string.IsNullOrEmpty(user.IMG) ? $"{baseImageUrl}/{user.IMG}": "~/Images/default.png";

            }
        }

        #endregion





        #region Button Events

        /// <summary>
        /// Handles the click event of the back button. Redirects to the main task page.
        /// </summary>

        protected void btnBackk_Click(object sender, EventArgs e)
        {


            Response.Redirect("TASK1.aspx");
        }
        #endregion
    }

}
