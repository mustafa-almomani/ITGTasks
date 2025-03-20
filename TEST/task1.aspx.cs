using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST.classes;
using TEST.ServiceReference1;
using static TEST.LOGIN;
namespace TEST
{
    public partial class task1 : System.Web.UI.Page
    {

        #region Page Load Event
        /// <summary>
        /// This method runs when the page loads. It binds data and loads the languages.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("Login.aspx");
              
            }

            gvAnnualReport.DataSourceArrayMethod = GetUsers;

            if (!IsPostBack)
            {
                LoadLanguages(DropDownListLanguages);

                title.InnerText = GetLocalResourceObject("title").ToString();

                BindData();
            }
        }
        #endregion

        #region Methods for Data Handling
        /// <summary>
        /// Fetches the list of users from the database.
        /// </summary>
        private User[] GetUsers()
        {
            ITGClass helper = new ITGClass();
            List<User> users = helper.GetUsers();

            if (users == null || users.Count == 0)
            {
                btnDeleteSelected.Visible = false;
                return new User[0];
            }

            btnDeleteSelected.Visible = true;
            return users.ToArray();
        }
        #endregion



        #region Bind Data
        /// <summary>
        /// Binds data to the GridView if the user is logged in. Otherwise, redirects to the login page.
        /// </summary>
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
        #endregion
        #region Events :: gvAnnualReport_RowCommand
        /// <summary>
        /// Handles various commands in the GridView such as Edit, Delete, and Update for user data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments containing command details.</param>
        protected void gvAnnualReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Control control = e.CommandSource as Control;

            if (control == null)
            {
                lblError.Text = "Error: Unable to identify the source of the command.";
                return;
            }

            GridViewRow row = control.NamingContainer as GridViewRow;
            if (row == null)
            {
                return;
            }

            int rowIndex = row.RowIndex;

            // 🔹 تحرير المستخدم في صفحة جديدة
            if (e.CommandName == "EditAnnualReport")
            {
                if (Session["LoggedInUser"] != null)
                {
                    User loggedInUser = (User)Session["LoggedInUser"];

                    if (loggedInUser.UserType == UserType.Admin)
                    {
                        int userID = Convert.ToInt32(gvAnnualReport.DataKeys[rowIndex].Value);
                        string encryptedUserID = EncryptionHelper.EncryptData(userID.ToString(), "YourSecretKey12345");
                        Response.Redirect("EditUser.aspx?userID=" + encryptedUserID);
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

            // 🔹 إلغاء وضع التحرير
            else if (e.CommandName == "CancelAnnualReport")
            {
                gvAnnualReport.EditIndex = -1;
                BindData();
            }

            // 🔹 حذف المستخدم
            else if (e.CommandName == "DeleteAnnualReport")
            {

                int userId = Convert.ToInt32(gvAnnualReport.DataKeys[rowIndex].Value);
                TASK1 taskService = new TASK1();
                taskService.DeleteUser(userId);
                BindData();
            }

            // 🔹 تفعيل وضع التحرير للصف المحدد
            else if (e.CommandName == "Editinside")
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

            else if (e.CommandName == "UpdateAnnualReport")
            {
                if (Session["LoggedInUser"] != null)
                {
                    User loggedInUser = (User)Session["LoggedInUser"];

                    if (loggedInUser.UserType == UserType.Admin)
                    {
                        int nuserID = Convert.ToInt32(gvAnnualReport.DataKeys[rowIndex].Value);
                        string sclientName = ((TextBox)row.FindControl("tbClientNameEdit")).Text;
                        string sbirthday = ((TextBox)row.FindControl("tbBirthday")).Text;
                        string sgender = ((DropDownList)row.FindControl("ddlGender")).SelectedValue;
                        DropDownList ddlLanguages = (DropDownList)row.FindControl("ddlLanguages");
                        string sLang_Id = ddlLanguages != null ? ddlLanguages.SelectedValue : "0";
                        string sNational_ID = ((TextBox)row.FindControl("tbNational_Id")).Text;

                        User user = TASK1.GetUserById(nuserID);
                        string sImg = user != null && !string.IsNullOrEmpty(user.IMG) ? user.IMG : "/ExternalImages/default.png";

                        FileUpload fileUpload = (FileUpload)row.FindControl("fileUploadControl");
                        if (fileUpload != null && fileUpload.HasFile)
                        {
                            try
                            {
                                string fileName = Path.GetFileName(fileUpload.FileName);
                                string extension = Path.GetExtension(fileName).ToLower();
                                List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

                                if (!allowedExtensions.Contains(extension))
                                {
                                    lblError.Text = "❌ Only JPG, JPEG, and PNG files are allowed.";
                                    lblError.ForeColor = System.Drawing.Color.Red;
                                    return;
                                }

                                byte[] fileBytes = fileUpload.FileBytes;
                                string base64String = Convert.ToBase64String(fileBytes);

                                using (ImageServiceSoapClient client = new ImageServiceSoapClient())
                                {
                                    string result = client.UploadImage(base64String, fileName);

                                    if (result.StartsWith("C:\\"))
                                    {
                                        sImg = fileName;
                                    }
                                    else
                                    {
                                        lblError.Text = "❌ Error uploading image: " + result;
                                        lblError.ForeColor = System.Drawing.Color.Red;
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lblError.Text = "❌ Error uploading image: " + ex.Message;
                                lblError.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }

                        TASK1.UpdateAnnualReport(nuserID, sclientName, sbirthday, sgender, Convert.ToInt32(sLang_Id), sImg, Convert.ToInt32(sNational_ID));

                        gvAnnualReport.EditIndex = -1;
                        BindData();

                        // ✅ SweetAlert Success Popup
                        string script = @"Swal.fire({
                                icon: 'success',
                                title: 'Update Successful',
                                text: ' User data has been updated successfully.',
                                confirmButtonText: 'OK'
                              });";

                        ScriptManager.RegisterStartupScript(this, GetType(), "UpdateSuccess", script, true);
                    }
                    else
                    {
                        lblError.Text = " You do not have the required permissions to update user data.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblError.Text = " You must be logged in to update user data.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        #endregion
        #region Button Click Event: btnAdd_Click
        /// <summary>
        /// Handles the click event of the "Add" button. This method validates the input data, 
        /// uploads the image if available, and then adds a new user to the system.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string suserName = tbUserName.Text.Trim();
                string sclientName = tbClientName.Text.Trim();
                string sgender = rblGender.SelectedValue;
                DateTime dtibirthday;

                string spassword = tbpassword.Text.Trim();
                string sEmail = tbEmail.Text.Trim();
                string sImg = "/ExternalImages/default.png";
                int nLang_Id = int.Parse(DropDownListLanguages.SelectedValue);
                int NationalId = Convert.ToInt32(tbNationalId.Text);

                if (string.IsNullOrEmpty(tbBirthday.Text.Trim()) ||
                    !DateTime.TryParseExact(tbBirthday.Text.Trim(),
                                            new string[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" },
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out dtibirthday))
                {
                    lblError.Text = "❌ Error: Please provide a valid Birthday!";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (fileUploadControl.HasFile)
                {
                    try
                    {
                        string fileName = Path.GetFileName(fileUploadControl.FileName);
                        string extension = Path.GetExtension(fileName).ToLower();

                        List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
                        if (!allowedExtensions.Contains(extension))
                        {
                            lblError.Text = "❌ Only JPG, JPEG, and PNG files are allowed.";
                            lblError.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        byte[] fileBytes = fileUploadControl.FileBytes;
                        string base64String = Convert.ToBase64String(fileBytes);

                        using (ImageServiceSoapClient client = new ImageServiceSoapClient("ImageServiceSoap"))
                        {
                            string result = client.UploadImage(base64String, fileName);

                            if (result.StartsWith("C:\\"))
                            {
                                sImg = fileName;
                            }
                            else
                            {
                                lblError.Text = "❌ Error uploading image: " + result;
                                lblError.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "❌ Error uploading image: " + ex.Message;
                        lblError.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }


                string addUserResult = TASK1.AddUser(suserName, sclientName, sgender, dtibirthday, UserType.User, spassword, sEmail, sImg, nLang_Id, NationalId);

                if (addUserResult == "User added successfully!")
                {
                    Response.Redirect(Request.RawUrl, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblError.Text = addUserResult;
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ An error occurred: " + ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }

        #endregion

        #region Button Click Event: btnSearch_Click
        /// <summary>
        /// Handles the click event of the "Search" button. This method fetches the list of users 
        /// based on the search criteria (gender and username) and displays them in a GridView.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sgender = ddlSearchGender.SelectedValue;
            string suserName = txtSearchUserName.Text.Trim();

            try
            {
                DataTable dtUsers = TASK1.SearchUsers(sgender, suserName);

                if (dtUsers.Rows.Count > 0)
                {
                    List<User> userList = new List<User>();

                    foreach (DataRow row in dtUsers.Rows)
                    {
                        userList.Add(new User
                        {
                            Id = Convert.ToInt32(row["id"]),
                            UserName = row["userNAME"].ToString(),
                            Email = row["Email"].ToString(),
                            Gender = row["Gender"].ToString(),
                            ClientName = row["clientNAME"].ToString(),
                            Birthday = Convert.ToDateTime(row["Birthday"]),
                            //Language = row["Language"].ToString(),
                            UserType = (UserType)Enum.Parse(typeof(UserType), row["usertype"].ToString()),
                            IMG = row["IMG"].ToString(),
                            LanguageName = row["LanguageName"].ToString(),
                            National_ID = Convert.ToInt32(row["National_ID"])

                        });
                    }

                    gvAnnualReport.DataSourceArrayMethod = () => userList.ToArray();
                    gvAnnualReport.DataBind();
                }
                else
                {
                    gvAnnualReport.DataSourceArrayMethod = null;
                    gvAnnualReport.DataBind();
                    lblError.Text = "No results found.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }

        #endregion

        #region WebMethod: DeleteUsers
        /// <summary>
        /// Deletes a list of users based on the provided user IDs.
        /// This method is exposed as a WebMethod for AJAX requests.
        /// </summary>
        /// <param name="userIds">List of user IDs to delete.</param>
        /// <returns>A string message indicating the result of the deletion process.</returns>


        [WebMethod]
        public static string DeleteUsers(List<int> userIds)
        {
            TASK1 taskService = new TASK1();
            if (userIds == null || userIds.Count == 0)
            {
                return "No users selected.";
            }

            try
            {

                foreach (int id in userIds)
                {

                    taskService.DeleteUser(id);
                }

                return $"{userIds.Count} users deleted successfully!";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        #endregion

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

        #region  gvAnnualReport_RowDataBound

        /// <summary>
        /// Handles the RowDataBound event for the GridView. 
        /// It processes the data bound to each row and updates various controls such as drop-down lists and images.
        /// </summary>

        protected void gvAnnualReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string baseImageUrl = ConfigurationManager.AppSettings["BaseImageUrl"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlLanguages = (DropDownList)e.Row.FindControl("ddlLanguages");

                if (ddlLanguages != null)
                {
                    object langObj = DataBinder.Eval(e.Row.DataItem, "LanguageName");
                    string currentLanguage = langObj != null ? langObj.ToString() : string.Empty;

                    LoadLanguages(ddlLanguages, currentLanguage);
                }

                Image imgUser = e.Row.FindControl("imgUser") as Image;
                if (imgUser != null)
                {
                    string fileName = DataBinder.Eval(e.Row.DataItem, "Img")?.ToString();
                    imgUser.ImageUrl = !string.IsNullOrEmpty(fileName)? $"{baseImageUrl}/{fileName}": ResolveUrl("~/Images/default.png");
                }

                LinkButton lbtnEdit = e.Row.FindControl("lbtnEdit") as LinkButton;
                LinkButton lbtnDelete = e.Row.FindControl("lbtnDelete") as LinkButton;
                LinkButton lbtnUpdate = e.Row.FindControl("lbtnUpdate") as LinkButton;
                LinkButton lbtnCancel = e.Row.FindControl("lbtnCancel") as LinkButton;
                LinkButton lbtnEditinsite = e.Row.FindControl("lbtnEditinsite") as LinkButton;

                if (lbtnEdit != null && lbtnDelete != null && lbtnUpdate != null && lbtnCancel != null && lbtnEditinsite != null)
                {
                    if (gvAnnualReport.EditIndex == e.Row.RowIndex)
                    {
                        lbtnEdit.Visible = false;
                        lbtnDelete.Visible = false;
                        lbtnEditinsite.Visible = false;
                        lbtnUpdate.Visible = true;
                        lbtnCancel.Visible = true;
                    }
                    else
                    {
                        lbtnEdit.Visible = true;
                        lbtnDelete.Visible = true;
                        lbtnEditinsite.Visible = true;
                        lbtnUpdate.Visible = false;
                        lbtnCancel.Visible = false;
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow && gvAnnualReport.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlGender = e.Row.FindControl("ddlGender") as DropDownList;
                string gender = DataBinder.Eval(e.Row.DataItem, "Gender")?.ToString();

                if (ddlGender != null && !string.IsNullOrEmpty(gender))
                {
                    ddlGender.SelectedValue = gender;
                }
            }
        }

        #endregion

        #region GetImageUrl
        /// <summary>
        /// Gets the appropriate image URL based on the given image path.
        /// It checks different conditions such as if the image path is a URL, local path, or file path,
        /// and returns the corresponding URL. If no valid path is found, it returns a default image URL.
        /// </summary>
        protected string GetImageUrl(object imgPath)
        {
            if (imgPath == null || string.IsNullOrEmpty(imgPath.ToString()))
            {
                return ResolveUrl("~/Images/default.png");
            }

            string path = imgPath.ToString();

            if (path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return path;
            }

            if (path.StartsWith("/"))
            {
                return ResolveUrl(path);
            }

            if (path.Contains(":\\"))
            {
                try
                {
                    string webServiceUrl = ConfigurationManager.AppSettings["ExternalWebServiceUrl"];
                    if (string.IsNullOrEmpty(webServiceUrl))
                    {
                        throw new Exception("❌ Error: ExternalWebServiceUrl is not configured in Web.config.");
                    }

                    ImageService oimgService = new ImageService();
                    oimgService.Url = webServiceUrl;

                    string fileName = Path.GetFileName(path);

                    string imageUrl = oimgService.GetImage(fileName);

                    if (string.IsNullOrEmpty(imageUrl) || imageUrl.Contains("Error"))
                    {
                        throw new Exception("❌ Error: ImageService returned an invalid response.");
                    }

                    return imageUrl;
                }
                catch (Exception ex)
                {
                    return ResolveUrl("~/Images/default.png");
                }
            }


            return path;
        }

        #endregion

        #region gvAnnualReport_PageIndexChanging
        /// <summary>
        /// Handles the PageIndexChanging event of the GridView control. 
        /// This event is triggered when the user navigates to a different page in the GridView.
        /// </summary>
        protected void gvAnnualReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAnnualReport.PageIndex = e.NewPageIndex;
            BindData();
        }
        #endregion

        #region
        /// <summary>
        /// Loads the languages into the DropDownList control. 
        /// The DropDownList is populated with data fetched from the TASK1 service.
        /// </summary>
        //private void LoadLanguages()
        //{
        //    TASK1 languageService = new TASK1();
        //    DropDownListLanguages.DataSource = languageService.GetLanguages();
        //    DropDownListLanguages.DataTextField = "Text";
        //    DropDownListLanguages.DataValueField = "Value";
        //    DropDownListLanguages.DataBind();

        //    //DropDownListLanguages.Items.Insert(0, new ListItem("--  Select language --", "0"));
        //    DropDownListLanguages.Items.Insert(0, new ListItem(GetLocalResourceObject("ListItemResource33").ToString(), "0"));

        //}


        #endregion


        //private void LoadLanguages(DropDownList ddlLanguages, string selectedLanguage)
        //{
        //    TASK1 languageService = new TASK1();
        //    ddlLanguages.DataSource = languageService.GetLanguages();
        //    ddlLanguages.DataTextField = "Text";
        //    ddlLanguages.DataValueField = "Value";
        //    ddlLanguages.DataBind();

        //    ddlLanguages.Items.Insert(0, new ListItem(GetLocalResourceObject("ListItemResource33").ToString(), "0"));

        //    if (!string.IsNullOrEmpty(selectedLanguage))
        //    {
        //        ListItem selectedItem = ddlLanguages.Items.FindByText(selectedLanguage);
        //        if (selectedItem != null)
        //        {
        //            ddlLanguages.ClearSelection();
        //            selectedItem.Selected = true;
        //        }
        //    }
        //}


        #region Method: LoadLanguages
        ///<summary>
        ///The LoadLanguages method is designed to populate a DropDownList with a list of languages and optionally pre-select a specific language. It uses a service (TASK1) to retrieve the languages, binds them to the DropDownList, and inserts a default "Select Language" item at the top. If a language is specified, it selects that language from the list.
        /// </summary>
        private void LoadLanguages(DropDownList ddlLanguages, string selectedLanguage = "")
        {
            TASK1 languageService = new TASK1();
            ddlLanguages.DataSource = languageService.GetLanguages();
            ddlLanguages.DataTextField = "Text";
            ddlLanguages.DataValueField = "Value";
            ddlLanguages.DataBind();

            ddlLanguages.Items.Insert(0, new ListItem(GetLocalResourceObject("ListItemResource33").ToString(), "0"));

            // If a selected language is provided, try to select it
            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                ListItem selectedItem = ddlLanguages.Items.FindByText(selectedLanguage);
                if (selectedItem != null)
                {
                    ddlLanguages.ClearSelection();
                    selectedItem.Selected = true;
                }
            }
        }
        #endregion
    }
}
