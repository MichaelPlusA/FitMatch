using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Capstone.Web.DAL
{
    public class WorkoutSQLDAL : IWorkoutDAL
    {
        private string connectionString;

        public WorkoutSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddExercise(string name, string description, int id, string type)
        {
            //this is not finished
            string AddExerciseDAL = "INSERT INTO exercises (exercise_name, exercise_description, trainer_id,) VALUES (@name, @description, @trainerID)";
            string addToTable;

            if(type == "cardio")
            {
                addToTable = "INSERT INTO strength_exercises (exercise_id) VALUES (@exercise_id)";
            }



            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddExerciseDAL, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@trainerID", id);

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
            string AddPlanDAL = "INSERT INTO workout_plan (trainer_id, trainee_id, plan_notes, plan_name) VALUES (@trainer_id, @trainee_id, @plan_notes, @plan_name)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddPlanDAL, conn);
                cmd.Parameters.AddWithValue("@plan_name", insertPlan.PlanName);
                cmd.Parameters.AddWithValue("@trainee_id", insertPlan.ForTrainee);
                cmd.Parameters.AddWithValue("@trainer_id", insertPlan.ByTrainer);
                cmd.Parameters.AddWithValue("@plan_notes", insertPlan.Notes);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public List<Plan> GetPlans(int traineeID)
        {
            List<Plan> PlanList = new List<Plan>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = "SELECT * from workout_plan WHERE trainee_id = @trainee_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@trainee_ID", traineeID);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Plan planToAdd = MapRowToPlan(reader);

                        PlanList.Add(planToAdd);
                    }
                }
            }
            return PlanList;
        }

        private Plan MapRowToPlan(SqlDataReader reader)
        {
            return new Plan()
            {
                ByTrainer = Convert.ToInt32(reader["trainer_id"]),
                ForTrainee = Convert.ToInt32(reader["trainee_id"]),
                PlanName = Convert.ToString(reader["plan_name"]),
                Notes = Convert.ToString(reader["plan_notes"]),
            };
        }
    }
}