using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TEST.classes
{
    public class ITGClass
    {
        #region GetUsers Method
        /// <summary>
        /// Retrieves a list of users from the database by executing the stored procedure "getusers".
        /// </summary>
        /// <returns>A list of <see cref="User"/> objects populated with data from the database.</returns>
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            // Get connection string from configuration
            string cs = ConfigurationManager.ConnectionStrings["Task1ConnectionString"].ConnectionString;

            // Create and open the SQL connection
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getusers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                // Execute the query and read the results
                SqlDataReader dr = cmd.ExecuteReader();

                // Loop through the results and create User objects
                while (dr.Read())
                {
                    users.Add(new User
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        UserName = dr["userNAME"].ToString(),
                        ClientName = dr["clientNAME"].ToString(),
                        Birthday = dr["Birthday"] != DBNull.Value ? Convert.ToDateTime(dr["Birthday"]) : DateTime.MinValue,
                        Gender = dr["Gender"].ToString(),
                        UserType = Enum.TryParse(dr["usertype"].ToString(), true, out UserType parsedType) ? parsedType : UserType.User,
                        LanguageName = dr["LanguageName"].ToString(),
                        Email = dr["Email"].ToString(),
                        IMG = dr["IMG"].ToString(),
                        National_ID = Convert.ToInt32(dr["National_ID"])
                    });
                }
            }

            return users;
        }
        #endregion
    }
}
