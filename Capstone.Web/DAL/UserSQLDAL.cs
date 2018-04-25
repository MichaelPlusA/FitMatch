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

        //non-trainer registration function
        public bool RegisterUser(User newUser)
        {
            string RegisterUserSQL = "INSERT INTO user_info (email, password, first_name, last_name, user_location, salt) VALUES (@email, @password, @FirstName, @LastName, @Location, @salt)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(RegisterUserSQL, conn);
                cmd.Parameters.AddWithValue("@Email", newUser.Email);
                cmd.Parameters.AddWithValue("@UserID", newUser.UserID);
                cmd.Parameters.AddWithValue("@Location", newUser.User_Location);
                cmd.Parameters.AddWithValue("@FirstName", newUser.First_Name);
                cmd.Parameters.AddWithValue("@LastName", newUser.Last_Name);
                cmd.Parameters.AddWithValue("@password", newUser.Password);
                cmd.Parameters.AddWithValue("@salt", newUser.Salt);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
    
            }
                return check;
        }

        //trainer registration function
        public bool RegisterUser(Trainer trainMaster)
        {
            bool check;

            string CreateUserSQL = "INSERT INTO user_info (email, password, salt, trainer_id, first_name, last_name) " +
                "VALUES (@email, @password, @salt, @first_name, @trainer_id, @last_name)";

            string createProfileSQL = "INSERT INTO trainer (price_per_hour, certifications, experience, client_success_stories, exercise_philosophy, additional_notes) " +
                "VALUES (@price_per_hour, @certifications, @experience, @client_success_stories, @exercise_philosophy, @additional_notes)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd2 = new SqlCommand(createProfileSQL, conn);
                
                cmd2.Parameters.AddWithValue("@price_per_hour", trainMaster.PricePerHour);
                cmd2.Parameters.AddWithValue("@exercise_philosophy", trainMaster.Philosophy);
                cmd2.Parameters.AddWithValue("@additional_notes", trainMaster.Additional_notes);
                cmd2.Parameters.AddWithValue("@experience", trainMaster.YearsExp);
                cmd2.Parameters.AddWithValue("@certifications", trainMaster.Certifications);
                cmd2.Parameters.AddWithValue("@client_success_stories", trainMaster.ClientSuccessStories);

                int mostRecent = (int)(cmd2.ExecuteScalar()); 

                SqlCommand cmd = new SqlCommand(CreateUserSQL, conn);
                cmd.Parameters.AddWithValue("@email", trainMaster.Email);
                cmd.Parameters.AddWithValue("@password", trainMaster.Password);
                cmd.Parameters.AddWithValue("@salt", trainMaster.Salt);
                cmd.Parameters.AddWithValue("@first_name", trainMaster.First_Name);
                cmd.Parameters.AddWithValue("@last_name", trainMaster.Last_Name);
                cmd.Parameters.AddWithValue("@trainer_id", mostRecent);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
                   
            }

            return check;
        }

        //login function
        public User GetCurrentUser(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                User result = conn.QueryFirstOrDefault<User>("Select email, password, salt, first_name, last_name, user_location, trainer_id FROM user_info WHERE email = @emailValue", new { emailValue = email });
                return result;
            }
        }
    }
}