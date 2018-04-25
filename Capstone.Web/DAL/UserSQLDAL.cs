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

        //trainer registration function
        public bool CreateTrainer(Trainer trainMaster)
        {
            bool check;
            string createProfileSQL = "INSERT INTO trainer (email, password, salt, first_name, last_name, user_location, price_per_hour, certifications, experience, client_success_stories, exercise_philosophy, additional_notes) " +
                "VALUES (@email, @password, @salt, @first_name, @last_name, @user_location, @certifications, @experience, @client_success_stories, @exercise_philosophy, @additional_notes)";

            string updateUser = "UPDATE user_info set trainer_id = @mostRecent where email = @email2";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(createProfileSQL, conn);
                cmd.Parameters.AddWithValue("@email", trainMaster.Email);
                cmd.Parameters.AddWithValue("@password", trainMaster.Password);
                cmd.Parameters.AddWithValue("@salt", trainMaster.Salt);
                cmd.Parameters.AddWithValue("@first_name", trainMaster.First_Name);
                cmd.Parameters.AddWithValue("@last_name", trainMaster.Last_Name);
                cmd.Parameters.AddWithValue("@user_location", trainMaster.User_Location);
                cmd.Parameters.AddWithValue("@exercise_philosophy", trainMaster.Philosophy);
                cmd.Parameters.AddWithValue("@price_per_hour", trainMaster.PricePerHour);
                cmd.Parameters.AddWithValue("@additional_notes", trainMaster.Additional_notes);
                cmd.Parameters.AddWithValue("@experience", trainMaster.YearsExp);
                cmd.Parameters.AddWithValue("@certifications", trainMaster.Certifications);
                cmd.Parameters.AddWithValue("@client_success_stories", trainMaster.ClientSuccessStories);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
                int mostRecent = Convert.ToInt32(cmd.ExecuteScalar());

                SqlCommand cmd2 = new SqlCommand(updateUser, conn);
                cmd2.Parameters.AddWithValue("@mostRecent", mostRecent);
                cmd2.Parameters.AddWithValue("@email2", trainMaster.Email);
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