using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TEST
{
    public class Login
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;



        #region AuthenticateUser Method

        /// <summary>
        /// Authenticates the user by checking their username and password against the database.
        /// </summary>
        /// <param name="username">The username provided by the user.</param>
        /// <param name="password">The password provided by the user.</param>
        /// <returns>A <see cref="User"/> object if the authentication is successful, otherwise null.</returns>
        public User AuthenticateUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("login", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    User loggedInUser = new User
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        UserName = reader["userNAME"].ToString(),
                        ClientName = reader["clientNAME"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Birthday = Convert.ToDateTime(reader["Birthday"]),
                        UserType = Enum.TryParse(reader["userType"].ToString(), true, out UserType parsedType)
    ? parsedType
    : UserType.Guest,
                        Password = reader["password"].ToString()
                    };
                    return loggedInUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    #endregion


}