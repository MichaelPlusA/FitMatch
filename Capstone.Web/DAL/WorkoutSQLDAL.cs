using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class WorkoutSQLDAL
    {
        private string connectionString;

        public WorkoutSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddExercise(string name, string description, string videoLink, string type)
        {
            string AddExerciseDAL = "";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddExerciseDAL, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desciption", description);
                cmd.Parameters.AddWithValue("@videoLink", videoLink);
                cmd.Parameters.AddWithValue("@type", type);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }



        //public List<Exercise> GetAllExercises()
        //{
        //    string GetAllExercisesDAL = "";

        //    List<Exercise> exercises = new List<Exercise>();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(//)
        //    }
        //}

        //public Exercise GetExerciseFromReader(SqlDataReader reader)
        //{
        //    Exercise exercise = new Exercise()
        //    {

        //    };
        //}
    }
}