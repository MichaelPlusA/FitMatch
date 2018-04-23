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
    public class RegisterDAL : IRegisterDAL
    {
        private string connectionString;

        public RegisterDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool RegisterUser(User newUser)
        {
            string RegisterUserSQL = "";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(RegisterUserSQL, conn);
                cmd.Parameters.AddWithValue("@Email", newUser.Email);
                cmd.Parameters.AddWithValue("@UserID", newUser.UserID);
                cmd.Parameters.AddWithValue("@Location", newUser.Location);
                cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newUser.LastName);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
    
            }
                return check;
        }

        public User GetCurrentUser(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                User result = conn.QueryFirstOrDefault<User>("Select * FROM users WHERE email = @emailValue", new { emailValue = email });
                return result;
            }
        }
    }
}