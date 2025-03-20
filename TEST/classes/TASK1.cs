using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TEST;

public class TASK1
{


    #region GetUsers Method

    /// <summary>
    /// Retrieves all users from the database using the "getusers" stored procedure.
    /// </summary>
    /// <returns>A DataTable containing all users' data.</returns>
    public DataTable GetUsers()
    {
        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("getusers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

    }
    #endregion




    #region UpdateUser Method

    /// <summary>
    /// Updates the user information in the database.
    /// </summary>
    /// <param name="gvUsers">The GridView containing user data.</param>
    /// <param name="e">The event arguments for GridView update.</param>
    /// <param name="lblError">The label for displaying error messages.</param>
    public static void UpdateUser(GridView gvUsers, GridViewUpdateEventArgs e, Label lblError)
    {
        GridViewRow row = gvUsers.Rows[e.RowIndex];

        TextBox txtUserName = row.FindControl("lblUserNameEdit") as TextBox;
        TextBox txtClientName = row.FindControl("tbClientNameEdit") as TextBox;
        DropDownList ddlGender = row.FindControl("ddlGender") as DropDownList;
        TextBox txtBirthday = row.FindControl("tbBirthday") as TextBox;
        TextBox txtLanguage = row.FindControl("tbLanguage") as TextBox;
        TextBox txtUserType = row.FindControl("tbusertype") as TextBox;

        if (txtUserName != null && txtClientName != null && ddlGender != null && txtBirthday != null && txtLanguage != null && txtUserType != null)
        {
            DateTime dtibirthday;
            if (string.IsNullOrWhiteSpace(txtBirthday.Text) || !DateTime.TryParseExact(txtBirthday.Text, "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US", false)
            {
                DateTimeFormat = { Calendar = new System.Globalization.GregorianCalendar() }
            },
         System.Globalization.DateTimeStyles.None, out dtibirthday))
            {
                lblError.Text = "Error: Invalid Birthday!";
                return;
            }

            int nuserID = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Values["id"]);
            string suserName = txtUserName.Text;
            string sclientName = txtClientName.Text;
            string sgender = ddlGender.SelectedValue;
            string slanguage = txtLanguage.Text;
            string suserType = txtUserType.Text;

            string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Updateusers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", nuserID);
                cmd.Parameters.AddWithValue("@userNAME", suserName);
                cmd.Parameters.AddWithValue("@clientNAME", sclientName);
                cmd.Parameters.AddWithValue("@Gender", sgender);
                cmd.Parameters.AddWithValue("@Birthday", dtibirthday);
                cmd.Parameters.AddWithValue("@Language", slanguage);
                cmd.Parameters.AddWithValue("@usertype", suserType);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvUsers.EditIndex = -1;
            BindData(gvUsers);
        }
        else
        {
            lblError.Text = "Error: One or more fields are missing!";
        }
    }

    #endregion


    /// <summary>
    /// Binds data to the GridView.
    /// </summary>
    /// <param name="gvUsers">The GridView to bind data to.</param>
    private static void BindData(GridView gvUsers)
    {
        TASK1 helper = new TASK1();
        var users = helper.GetUsers();
        gvUsers.DataSource = users;
        gvUsers.DataBind();
    }

    #region DeleteUser Method
    /// <summary>
    /// Deletes a user from the database using the "Deleteusers" stored procedure.
    /// </summary>
    /// <param name="userId">The ID of the user to be deleted.</param>
    public void DeleteUser(int userId)
    {
        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("Deleteusers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", userId);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    #endregion

    #region AddUser Method

    /// <summary>
    /// Adds a new user to the database using the "Insertusers" stored procedure.
    /// </summary>
    /// <param name="suserName">The username of the user.</param>
    /// <param name="sclientName">The client name of the user.</param>
    /// <param name="sgender">The gender of the user.</param>
    /// <param name="dtibirthday">The birthday of the user.</param>
    /// <param name="userType">The type of user (Admin, User, etc.).</param>
    /// <param name="spassword">The password of the user.</param>
    /// <param name="sEmail">The email address of the user.</param>
    /// <param name="sImg">The image URL of the user.</param>
    /// <param name="nLang_Id">The ID of the user's language.</param>
    /// <param name="NationalId">The National ID of the user.</param>
    /// <returns>A string indicating success or failure.</returns>
    public static string AddUser(string suserName, string sclientName, string sgender, DateTime dtibirthday, UserType userType, string spassword, string sEmail, string sImg, int nLang_Id ,int NationalId)
    {
         int userTypeValue = (int)userType;
        if (string.IsNullOrEmpty(suserName))
            return "Error: User Name is required!";
        if (string.IsNullOrEmpty(sclientName))
            return "Error: Client Name is required!";
        if (string.IsNullOrEmpty(sgender))
            return "Error: Gender is required!";
        if (nLang_Id == null )
            return "Error: Language is required!";
        //if (string.IsNullOrEmpty(suserType))
        //    return "Error: User Type is required!";
        if (dtibirthday == DateTime.MinValue)
            return "Error: Please provide a valid Birthday!";
        if (string.IsNullOrEmpty(sEmail))
            return "Error: Email is required!";
        if (NationalId==null )
            return "Error: Email is required!";
        if (NationalId < 9)
            return "please enter 9 digits";

        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM users WHERE userNAME = @userNAME", con);
            checkCmd.Parameters.AddWithValue("@userNAME", suserName);
            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                return "Error: User Name already exists!"; 
            }

           
            SqlCommand cmd = new SqlCommand("Insertusers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userNAME", suserName);
            cmd.Parameters.AddWithValue("@clientNAME", sclientName);
            cmd.Parameters.AddWithValue("@Gender", sgender);
            cmd.Parameters.AddWithValue("@Birthday", dtibirthday);
            cmd.Parameters.AddWithValue("@usertype", userTypeValue);
            cmd.Parameters.AddWithValue("@password", spassword);
            cmd.Parameters.AddWithValue("@Email", sEmail);
            cmd.Parameters.AddWithValue("@IMG", sImg);
            cmd.Parameters.AddWithValue("lang_ID", nLang_Id);
            cmd.Parameters.AddWithValue("National_ID", NationalId);

            try
            {
                cmd.ExecuteNonQuery();
                return "User added successfully!";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }

    #endregion

    #region SearchUsers Method

    /// <summary>
    /// Searches users by gender and username using the "SearchUsersByNameAndGender" stored procedure.
    /// </summary>
    /// <param name="sgender">The gender to search for.</param>
    /// <param name="suserName">The username to search for.</param>
    /// <returns>A DataTable containing the matching users.</returns>
    public static DataTable SearchUsers(string sgender, string suserName)
    {
        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
        DataTable dtUsers = new DataTable();

        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("SearchUsersByNameAndGender", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(sgender) ? (object)DBNull.Value : sgender);
            cmd.Parameters.AddWithValue("@UserName", string.IsNullOrEmpty(suserName) ? (object)DBNull.Value : suserName);

            try
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dtUsers.Load(dr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        return dtUsers;
    }

    #endregion
    #region UpdateAnnualReport Method

    /// <summary>
    /// Updates the annual report for a user using the "UpdateUsers" stored procedure.
    /// </summary>
    /// <param name="nuserID">The user ID to update.</param>
    /// <param name="sclientName">The new client name.</param>
    /// <param name="dtibirthday">The new birthday.</param>
    /// <param name="sgender">The new gender.</param>
    /// <param name="nlanguage">The language ID.</param>
    /// <param name="sImg">The new image URL.</param>
    /// <param name="National_ID">The new national ID.</param>
    public static void UpdateAnnualReport(int nuserID, string sclientName,  string dtibirthday, string sgender, int nlanguage ,string sImg ,int National_ID )
    {
        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateUsers", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", nuserID);
                //cmd.Parameters.AddWithValue("@userNAME", suserName);
                cmd.Parameters.AddWithValue("@clientNAME", sclientName);
                cmd.Parameters.AddWithValue("@Gender", sgender);
                cmd.Parameters.AddWithValue("@Birthday", dtibirthday);
                cmd.Parameters.AddWithValue("@lang_ID", nlanguage);
                //cmd.Parameters.AddWithValue("@usertype", suserType);
                cmd.Parameters.AddWithValue("IMG", sImg);
                cmd.Parameters.AddWithValue("National_ID", National_ID);


                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    #endregion


    #region GetUserById Method

    /// <summary>
    /// Retrieves a user by their ID using the "GetUserById" stored procedure.
    /// </summary>
    /// <param name="nuserId">The ID of the user to retrieve.</param>
    /// <returns>A User object containing the user's details.</returns>

    public static User GetUserById(int nuserId)
    {
        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
        User user = null;

        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("GetUserById", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", nuserId);

            try
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user = new User();

                            user.Id = Convert.ToInt32(dr["id"]);
                                user.UserName = dr["userName"].ToString();
                                user.ClientName = dr["clientName"].ToString();
                            user.Birthday = dr["Birthday"] != DBNull.Value ? Convert.ToDateTime(dr["Birthday"]) : DateTime.MinValue;
                            user.Gender = dr["Gender"].ToString();
                                user.UserType = (UserType)Convert.ToInt32(dr["usertype"]);
                               user.LanguageName = dr["LanguageName"].ToString();
                                user.LanguageID = Convert.ToInt32(dr["lang_ID"]);
                                user.IMG = dr["IMG"].ToString();
                                user.National_ID = Convert.ToInt32(dr["National_ID"]);
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        return user;
    }


    #endregion
    #region Methods

    /// <summary>
    /// Retrieves a list of available languages from the database by calling the stored procedure "GetAllLanguages".
    /// Each language is returned as a ListItem with a text label and corresponding value.
    /// </summary>
    /// <returns>A list of languages represented as ListItem objects.</returns>
    public List<ListItem> GetLanguages()
    {
        List<ListItem> languages = new List<ListItem>();

        string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("GetAllLanguages", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    languages.Add(new ListItem
                    {
                        Text = reader["language"].ToString(),
                        Value = reader["id"].ToString()
                    });
                }
            }
        }

        return languages;
    }
    #endregion

}
