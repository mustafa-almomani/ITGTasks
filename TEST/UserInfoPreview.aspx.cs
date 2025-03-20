using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TEST.LOGIN;

namespace TEST
{
    public partial class UserInfoPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["UserId"] != null)
                {
                    int userId;
                    if (int.TryParse(Request.QueryString["UserId"], out userId))
                    {
                        UserDetailsControl.UserId = userId;
                        UserDetailsControl.LoadUserData();
                    }
                }
            }

        }

        
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


     


    }
}
