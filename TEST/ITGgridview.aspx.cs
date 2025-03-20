using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST;
using TEST.classes;
using TEST.ServiceReference1;

namespace TEST
{
    public partial class ITGgridview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvAnnualReport.DataSourceArrayMethod = RRyuaera;

            if (!IsPostBack)
            {
                BindData();
            }



        }






        protected void gvAnnualReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton lbtnUpdate = (LinkButton)e.Row.FindControl("lbtnUpdate");
                LinkButton lbtnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");

                if (gvAnnualReport.EditIndex == e.Row.RowIndex)
                {
                    lbtnEdit.Visible = false;
                    lbtnDelete.Visible = false;
                    lbtnUpdate.Visible = true;
                    lbtnCancel.Visible = true;
                }
                else
                {
                    lbtnEdit.Visible = true;
                    lbtnDelete.Visible = true;
                    lbtnUpdate.Visible = false;
                    lbtnCancel.Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow && gvAnnualReport.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlGender = (DropDownList)e.Row.FindControl("ddlGender");

                string gender = DataBinder.Eval(e.Row.DataItem, "Gender").ToString();

                if (ddlGender != null)
                {
                    ddlGender.SelectedValue = gender;
                }
            }
        }




        private void BindData()
        {
            if (Session["LoggedInUser"] != null)
            {


                gvAnnualReport.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private User[] RRyuaera()
        {
            ITGClass helper = new ITGClass();
            List<User> users = helper.GetUsers();
            return users.ToArray();
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

        protected void gvAnnualReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;

            if (e.CommandName == "EditAnnualReport")
            {
                if (Session["LoggedInUser"] != null)
                {
                    User loggedInUser = (User)Session["LoggedInUser"];

                    if (loggedInUser.UserType == UserType.Admin)
                    {
                        gvAnnualReport.EditIndex = rowIndex;
                        BindData();
                    }
                    else
                    {
                        lblError.Text = "You do not have the required permissions to edit user data.";
                    }
                }
                else
                {
                    lblError.Text = "You must be logged in to edit user data.";
                }
            }
            else if (e.CommandName == "CancelAnnualReport")
            {
                gvAnnualReport.EditIndex = -1;
                BindData();
            }
            else if (e.CommandName == "DeleteAnnualReport")
            {

                int userId = Convert.ToInt32(gvAnnualReport.DataKeys[rowIndex].Value);
                TASK1 taskService = new TASK1();
                taskService.DeleteUser(userId);
                BindData();
            }
            else if (e.CommandName == "UpdateAnnualReport")
            {
                if (Session["LoggedInUser"] != null)
                {
                    User loggedInUser = (User)Session["LoggedInUser"];

                    if (loggedInUser.UserType == UserType.Admin)
                    {
                        int userID = Convert.ToInt32(gvAnnualReport.DataKeys[rowIndex].Value);
                        string userName = ((TextBox)gvAnnualReport.Rows[rowIndex].FindControl("lblUserNameEdit")).Text;
                        string clientName = ((TextBox)gvAnnualReport.Rows[rowIndex].FindControl("tbClientNameEdit")).Text;
                        string userType = ((TextBox)gvAnnualReport.Rows[rowIndex].FindControl("tbusertype")).Text;
                        string birthday = ((TextBox)gvAnnualReport.Rows[rowIndex].FindControl("tbBirthday")).Text;
                        string gender = ((DropDownList)gvAnnualReport.Rows[rowIndex].FindControl("ddlGender")).SelectedValue;
                        //int language = ((TextBox)gvAnnualReport.Rows[rowIndex].FindControl("tbLanguage")).Text;
                        string sImg = "/ExternalImages/default.png";

                        //TASK1.UpdateAnnualReport(userID, clientName, birthday, gender, language, sImg);

                        gvAnnualReport.EditIndex = -1;
                        BindData();
                    }
                    else
                    {
                        lblError.Text = "You do not have the required permissions to update user data.";
                    }
                }
                else
                {
                    lblError.Text = "You must be logged in to update user data.";
                }
            }
        }





        //protected void gvAnnualReport_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    //gvAnnualReport.EditIndex = e.NewEditIndex;
        //    //BindData();
        //}


        //protected void gvAnnualReport_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gvAnnualReport.EditIndex = -1;
        //    BindData(); 
        //}




        //protected void gvAnnualReport_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    TASK1.UpdateUser(gvAnnualReport, e, lblError);
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUploadControl.HasFile)
            {
                try
                {
                    // 🔹 الحصول على بيانات الصورة
                    string fileName = Path.GetFileName(fileUploadControl.FileName);
                    string extension = Path.GetExtension(fileName).ToLower();

                    // 🔹 قائمة الامتدادات المسموح بها
                    List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

                    // 🔹 التحقق من نوع الملف (يجب أن يكون JPG أو JPEG أو PNG)
                    if (!allowedExtensions.Contains(extension))
                    {
                        lblMessage.Text = "❌ فقط ملفات JPG و JPEG و PNG مسموحة.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // 🔹 تحويل الصورة إلى Base64
                    byte[] fileBytes = fileUploadControl.FileBytes;
                    string base64String = Convert.ToBase64String(fileBytes);

                    // 🔹 استدعاء ويب سيرفس
                    ImageServiceSoapClient client = new ImageServiceSoapClient();
                    string result = client.UploadImage(base64String, fileName);

                    lblMessage.Text = "✅ " + result;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "❌ حدث خطأ أثناء رفع الملف: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMessage.Text = "❌ يرجى اختيار صورة.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}