using System;

namespace TEST
{



    #region Entities

    /// <summary>
    /// Represents a user in the application, including login credentials, personal details,
    /// and preferences such as language and profile image.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ClientName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public UserType UserType { get; set; } 


        //public string Language { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IMG { get; set; }
        public string LanguageName { get; set; }

        public int LanguageID { get; set; }
        public int National_ID { get; set; }
 
    }

    #endregion

    #region Enums

    /// <summary>
    /// Defines possible types of users in the system such as Admin, User, and Guest.
    /// </summary>

    public enum UserType
    {
        Admin = 1,
        User = 2,
        Guest = 3
    }
    #endregion
}



