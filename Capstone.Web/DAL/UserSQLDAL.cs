using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class UserSQLDAL : IUserDAL
    {
        private string connectionString;

        public UserSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool RegisterUser(User newUser)
        {
            string RegisterUserSQL = "INSERT INTO user_info (email, password, first_name, last_name, user_location, salt) VALUES (@email, @password, @FirstName, @LastName, @Location, @salt)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                newUser.Salt = "very salty";

                SqlCommand cmd = new SqlCommand(RegisterUserSQL, conn);
                cmd.Parameters.AddWithValue("@Email", newUser.Email);
                cmd.Parameters.AddWithValue("@UserID", newUser.UserID);
                SqlParameter locationParam = cmd.Parameters.AddWithValue("@Location", newUser.User_Location);
                if(newUser.User_Location == null)
                {
                    locationParam.Value = DBNull.Value;
                }
                cmd.Parameters.AddWithValue("@FirstName", newUser.First_Name);
                cmd.Parameters.AddWithValue("@LastName", newUser.Last_Name);
                cmd.Parameters.AddWithValue("@password", newUser.Password);
                cmd.Parameters.AddWithValue("@salt", newUser.Salt);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
    
            }
                return check;
        }

        public User GetCurrentUser(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                User result = conn.QueryFirstOrDefault<User>("Select email, password, salt, first_name, last_name, user_location FROM user_info WHERE email = @emailValue", new { emailValue = email });
                return result;
            }
        }
    }
}