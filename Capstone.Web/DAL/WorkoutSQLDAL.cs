using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class WorkoutSQLDAL : IWorkoutDAL
    {
        private string connectionString;

        public WorkoutSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddExercise(string name, string description, string videoLink, string type)
        {
            //this is not finished
            string AddExerciseDAL = "INSERT INTO exercises (exercise_name, exercise_description, vide_link, trainer_id) VALUES (@name, @description, @videolink, @type)";
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

        public bool AddWorkout(Workout Moves)
        {
            //workouts might need a exerciseID in the SQL table
            //this is not done
            string AddWorkoutDAL = "INSERT INTO workout (workout_name, additional_notes, plan_id)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddWorkoutDAL, conn);
                cmd.Parameters.AddWithValue("@", Moves.GetBig);
                cmd.Parameters.AddWithValue("@", Moves.RunningAndStuff);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public bool AddPlan(Plan insertPlan)
        {
            string AddPlanDAL = "INSERT INTO workout_plan (trainer_id, trainee_id, plan_notes) VALUES (@trainer_id, @trainee_id, @plan_notes)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddPlanDAL, conn);
                cmd.Parameters.AddWithValue("@trainee_id", insertPlan.ForTrainee);
                cmd.Parameters.AddWithValue("@trainer_id", insertPlan.ByTrainer);
                cmd.Parameters.AddWithValue("@plan_notes", insertPlan.Notes);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }
    }
}