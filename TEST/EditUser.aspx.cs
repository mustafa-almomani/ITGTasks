using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST.classes;
using TEST.ServiceReference1;
using System.Net.Http;
using System.Globalization;
using System.Threading;



namespace TEST
{
    public partial class EditUser : System.Web.UI.Page
    {

        #region Page_Load Method
        /// <summary>
        /// This method is called when the page loads. It handles the decryption of the user ID
        /// and loads user data if the user ID is valid.
        /// </summary>

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {



                title.InnerText = GetLocalResourceObject("title").ToString();


                LoadLanguages();
                string encryptedUserID = Request.QueryString["userID"];
                System.Diagnostics.Debug.WriteLine($"🔹 Encrypted UserID Received: {encryptedUserID}");

                if (!string.IsNullOrEmpty(encryptedUserID))
                {
                    string decodedUserID = HttpUtility.UrlDecode(encryptedUserID);
                    System.Diagnostics.Debug.WriteLine($"🔹 Decoded UserID: {decodedUserID}");

                    string decryptedUserID = EncryptionHelper.DecryptData(decodedUserID, "YourSecretKey12345");
                    System.Diagnostics.Debug.WriteLine($"🔹 Decrypted UserID: {decryptedUserID}");
                    hfUserId.Value = decryptedUserID;


                    if (!string.IsNullOrEmpty(decryptedUserID))
                    {

                        LoadUserData(decryptedUserID);
                    }
                    else
                    {
                        lblError.Text = "❌ Invalid user ID (Decryption failed).";
                    }
                }

            }
        }

        #endregion


        #region LoadUserData Method
        /// <summary>
        /// This method loads user data from the database using the provided user ID and populates the form fields.
        /// </summary>
        private void LoadUserData(string userID)
        {
            try
            {
                User user = TASK1.GetUserById(Convert.ToInt32(userID));

                if (user != null)
                {
                    tbUserName.Text = user.UserName;
                    tbClientName.Text = user.ClientName;
                    tbBirthday.Text = Convert.ToDateTime(user.Birthday)
                        .ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                    //tbLanguage.Text = user.Language;
                    rblGender.SelectedValue = user.Gender;
                    tbusertype444.Text = user.UserType.ToString();
                    DropDownListLanguages.SelectedValue = user.LanguageID.ToString();
                    tbNationalId.Text = user.National_ID.ToString();
                    imgUser.AlternateText= user.IMG.ToString();
               


                    if (user != null && !string.IsNullOrEmpty(user.IMG))
                    {
                        imgUser.ImageUrl = $"http://itg-mmmomani/ExternalImages/{user.IMG}";
                    }
                    else
                    {
                        imgUser.ImageUrl = ResolveUrl("~/Images/default.png");
                    }
                }
                else
                {
                    lblError.Text = "❌ User not found.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "❌ Error loading user data: " + ex.Message;
            }
        }

        #endregion


        #region btnAdd_Click Method
        /// <summary>
        /// This method is called when the "Add" button is clicked. It validates the form, processes user data,
        /// uploads an image (if provided), and updates the user information.
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {



        

            string encryptedUserID = Request.QueryString["userID"];
            if (!string.IsNullOrEmpty(encryptedUserID))
            {
                try
                {
                    string decryptedUserID = EncryptionHelper.DecryptData(encryptedUserID, "YourSecretKey12345");

                    if (!string.IsNullOrEmpty(decryptedUserID))
                    {
                        string userID = decryptedUserID;
                        string userName = tbUserName.Text;
                        string clientName = tbClientName.Text;
                        string userType = tbusertype444.Text;
                        string birthday = tbBirthday.Text;
                        string gender = rblGender.SelectedValue;
                        int nLang_Id = int.Parse(DropDownListLanguages.SelectedValue);
                        string National_ID = tbNationalId.Text;
                 


                        if (DropDownListLanguages.SelectedValue == "0")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert",
                                "Swal.fire({ icon: 'error', title: 'Validation Error', text: 'Please select a valid language.', confirmButtonText: 'OK' });", true);
                            return; 
                        }


                        User user = TASK1.GetUserById(Convert.ToInt32(userID));
                        if (!fileUploadControl.HasFile && (user == null || string.IsNullOrEmpty(user.IMG) || user.IMG == "/ExternalImages/default.png"))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert",
                                "Swal.fire({ icon: 'error', title: 'Validation Error', text: 'Please upload an image.', confirmButtonText: 'OK' });", true);
                            return;
                        }

                        string sImg = user != null && !string.IsNullOrEmpty(user.IMG) ? user.IMG : "/ExternalImages/default.png";

                    
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

                        TASK1.UpdateAnnualReport(Convert.ToInt32(userID), clientName, birthday, gender, Convert.ToInt32(nLang_Id), sImg, Convert.ToInt32(National_ID));

                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert",
                       "Swal.fire({ position: 'center', icon: 'success', title: 'User updated successfully!', showConfirmButton: false, timer: 1500 }).then(() => { window.location.href = 'TASK1.aspx'; });", true);




                    }
                    else
                    {
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Text = "Invalid user ID.";
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error processing user update: " + ex.Message;
                }
            }
            else
            {
                lblError.Text = "User ID is missing.";
            }
        }

        #endregion
        #region btnBackk_Click Method
        /// <summary>
        /// This method is called when the "Back" button is clicked. It redirects the user to the `TASK1.aspx` page.
        /// </summary>

        protected void btnBackk_Click(object sender, EventArgs e)
        {
            Response.Redirect("TASK1.aspx");
        }
        #endregion

        #region LoadLanguages Method
        /// <summary>
        /// This method loads available languages into the DropDownListLanguages dropdown control.
        /// </summary>
        private void LoadLanguages()
        {
            TASK1 languageService = new TASK1();
            DropDownListLanguages.DataSource = languageService.GetLanguages();
            DropDownListLanguages.DataTextField = "Text";
            DropDownListLanguages.DataValueField = "Value";
            DropDownListLanguages.DataBind();

            DropDownListLanguages.Items.Insert(0, new ListItem(GetLocalResourceObject("ListItemResource3").ToString(), "0"));
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








    }
}
