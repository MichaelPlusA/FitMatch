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
            string delimitedCerts = DelimitedList(trainMaster.ListCertifications);

            bool check;

            string CreateUserSQL = "INSERT INTO user_info (email, password, salt, trainer_id, first_name, last_name) " +
                "VALUES (@email, @password, @salt, @trainer_id, @first_name, @last_name)";

            string createProfileSQL = "INSERT INTO trainer (price_per_hour, certifications, experience, client_success_stories, exercise_philosophy, additional_notes) " +
                "OUTPUT inserted.trainer_id VALUES (@price_per_hour, @certifications, @experience, @client_success_stories, @exercise_philosophy, @additional_notes)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd2 = new SqlCommand(createProfileSQL, conn);
                
                cmd2.Parameters.AddWithValue("@price_per_hour", trainMaster.Price_Per_Hour);
                cmd2.Parameters.AddWithValue("@exercise_philosophy", trainMaster.exercise_Philosophy);
                cmd2.Parameters.AddWithValue("@additional_notes", trainMaster.Additional_notes);
                cmd2.Parameters.AddWithValue("@experience", trainMaster.YearsExp);
                cmd2.Parameters.AddWithValue("@certifications", delimitedCerts);
                cmd2.Parameters.AddWithValue("@client_success_stories", trainMaster.Client_Success_Stories);

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

        public Trainer GetTrainer(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Trainer result = conn.QueryFirstOrDefault<Trainer>("Select trainer_id, price_per_hour, experience, client_success_stories, exercise_philosophy, certifications, additional_notes FROM trainer WHERE trainer_id = @trainerID", new { trainerID = ID });
                return result;
            }
        }

        private string DelimitedList (List<string> list)
        {
            string delimitedList = "";

            foreach(string item in list)
            {
                delimitedList += item + "|";
            }

            return delimitedList;
        }

        public bool UpdateTrainer(Trainer update)
        {
            bool check;
            string UpdateTrainerSQL = "UPATE trainer SET price_per_hour = @price_per_hour, certifications = @certifications, experience = @experience, " +
                "client_success_stories = @client_success_stories, exercise_philosophy = @exercise_philosophy, @additional_notes = additional_notes WHERE trainer_id = @trainer_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(UpdateTrainerSQL, conn);
                cmd.Parameters.AddWithValue("@price_per_hour", update.Price_Per_Hour);
                cmd.Parameters.AddWithValue("@certifications", update.Certifications);
                cmd.Parameters.AddWithValue("@experience", update.YearsExp);
                cmd.Parameters.AddWithValue("@client_success_stories", update.Client_Success_Stories);
                cmd.Parameters.AddWithValue("@exercise_philosophy", update.exercise_Philosophy);
                cmd.Parameters.AddWithValue("@additional_notes", update.Additional_notes);
                cmd.Parameters.AddWithValue("@trainer_id", update.Trainer_ID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }
    }
}