﻿using Capstone.Web.DAL.Interfaces;
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

<<<<<<< HEAD
=======
        #region jon
        //public bool CreateTrainerProfile(Trainer trainMaster)
        //{
        //    bool check;
        //    string createProfileSQL = "";

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        SqlCommand cmd = new SqlCommand(createProfileSQL, conn);
        //        cmd.Parameters.AddWithValue("@", trainMaster.First_Name);
        //        cmd.Parameters.AddWithValue("@", trainMaster.Last_Name);
        //        cmd.Parameters.AddWithValue("@", trainMaster.Email);
        //        cmd.Parameters.AddWithValue("@", trainMaster.Password);
        //        cmd.Parameters.AddWithValue("@", trainMaster.Salt);
        //        cmd.Parameters.AddWithValue("@", trainMaster.)



        //    }
        //}
        #endregion
>>>>>>> 51a5ed4fe11a7e5ca628e3c5a8ee15f4d41b2133

        /// <summary>
        /// Search for trainer(s) by last name (required) and first name (optional), calls the DB for users with trainer IDs- PC
        /// </summary>
        /// <param name="trainerFirstName"></param>
        /// <param name="trainerLastName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Search for trainer(s) by price per hour, calls the DB for users with trainer IDs and joins trainer table - PC
        /// </summary>
        /// <param name="pricePerHour"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Helper method to create user objects from the DB - PC
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
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