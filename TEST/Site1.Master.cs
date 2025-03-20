using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static TEST.LOGIN;

namespace TEST
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyLanguageDirection();

            if (!IsPostBack) 
            {
                ddlLanguage.Items.Add(new ListItem("English", "en"));
                ddlLanguage.Items.Add(new ListItem("العربية", "ar"));

                if (Session["PreferredLanguage"] != null)
                {
                    ddlLanguage.SelectedValue = Session["PreferredLanguage"].ToString();
                }

                lbtnlogOut.InnerText = GetLocalResourceObject("lbtnlogOut").ToString();
                ManageUsers.InnerText = GetLocalResourceObject("ManageUsers").ToString();
                Homepage.InnerText = GetLocalResourceObject("Homepage").ToString();
                lbtnError.InnerText = GetLocalResourceObject("lbtnError").ToString();
            }

            if (Request.QueryString["action"] == "logout")
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("LOGIN.aspx");
            }

            if (Session["LoggedInUser"] != null)
            {
                User loggedInUser = (User)Session["LoggedInUser"];

                lbuserInformation.Text = loggedInUser.UserName;
               
                lbtnlogOut.HRef = "LOGIN.aspx?action=logout";
                if(loggedInUser.UserType.ToString() == "User")
                {
                    ManageUsers.Parent.Controls.Remove(ManageUsers);
                    //ITGgridView.Parent.Controls.Remove(ITGgridView);

                }
            }
            else
            {

                lbtnlogOut.HRef = "LOGIN.aspx";
            }

           
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PreferredLanguage"] = ddlLanguage.SelectedValue;

            Response.Redirect(Request.RawUrl);
        }

        private void ApplyLanguageDirection()
        {
            if (Session["PreferredLanguage"] != null && Session["PreferredLanguage"].ToString() == "ar")
            {
                Page.Header.Controls.Add(new LiteralControl(
                    "<style>body { direction: rtl; text-align: right; } .sidebar { left: auto; right: 0; } .content { margin-left: 0; margin-right: 250px; }</style>"));
            }
            else
            {
                Page.Header.Controls.Add(new LiteralControl(
                    "<style>body { direction: ltr; text-align: left; } .sidebar { left: 0; right: auto; } .content { margin-left: 250px; margin-right: 0; }</style>"));
            }
        }

    }
}
