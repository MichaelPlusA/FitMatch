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
    public class ProfileSQLDAL : IProfileDAL
    {

        private string connectionString;

        public ProfileSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> TrainerProfileSearchName(string trainerFirstName, string trainerLastName)
        {
            List<User> SearchList = new List<User>();

            string SQLSearchString = "user_id, select first_name, last_name, email from user_info where last_name = @last_name";

            if (trainerFirstName != null)
            {
                SQLSearchString += " and first_name = @first_name";
            }

            SQLSearchString += " and trainer_id IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQLSearchString, conn))
                {
                    cmd.Parameters.AddWithValue("@last_name", trainerLastName);

                    if (trainerFirstName != null)
                    {
                        cmd.Parameters.AddWithValue("first_name", trainerFirstName);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User userToAdd = MapRowToUser(reader);

                        SearchList.Add(userToAdd);
                    }
                }

            }
            return SearchList;
        }

        public List<User> TrainerProfileSearchPrice (int pricePerHour)
        {
            List<User> SearchList = new List<User>();

            string SQLSearchString = "select user_id, first_name, last_name, email from user_info" +
                " JOIN trainer on user_info.trainer_id = trainer.trainer_id WHERE price_per_hour <= @price_per_hour";

            SQLSearchString += " and trainer_id IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQLSearchString, conn))
                {
                    cmd.Parameters.AddWithValue("@price_per_hour", pricePerHour);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User userToAdd = MapRowToUser(reader);

                        SearchList.Add(userToAdd);
                    }
                }
            }
            return SearchList;
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            return new User()
            {
                UserID = Convert.ToInt32(reader["user_id"]),
                First_Name = Convert.ToString(reader["first_name"]),
                Last_Name = Convert.ToString(reader["last_name"]),
                Email = Convert.ToString(reader["email"]),
            };
        }
    }
}