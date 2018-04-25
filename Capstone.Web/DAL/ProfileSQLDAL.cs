using Capstone.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class ProfileSQLDAL
    {
        
        private string connectionString;

        public ProfileSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region jon
        public bool CreateTrainerProfile(Trainer trainMaster)
        {
            bool check;
            string createProfileSQL = "";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(createProfileSQL, conn);
                cmd.Parameters.AddWithValue("@", trainMaster.First_Name);
                cmd.Parameters.AddWithValue("@", trainMaster.Last_Name);
                cmd.Parameters.AddWithValue("@", trainMaster.Email);
                cmd.Parameters.AddWithValue("@", trainMaster.Password);
                cmd.Parameters.AddWithValue("@", trainMaster.Salt);
                cmd.Parameters.AddWithValue("@", trainMaster.)

               
            }
        }
        #endregion
    }
}