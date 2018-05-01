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

        public bool AddExercise(Exercise addExercise)
        {
            //this is not finished
            string AddExerciseDAL = "INSERT INTO exercises (exercise_name, exercise_description, trainer_id, exercise_type_id, video_link) VALUES (@name, @description, @trainerID, @exerciseTypeID, @videolink)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddExerciseDAL, conn);
                cmd.Parameters.AddWithValue("@name", addExercise.Name);
                cmd.Parameters.AddWithValue("@description", addExercise.Description);
                cmd.Parameters.AddWithValue("@trainerID", addExercise.TrainerID);
                cmd.Parameters.AddWithValue("@exerciseTypeID", addExercise.Type);
                cmd.Parameters.AddWithValue("@videolink", addExercise.VideoLink);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public bool AddCardioToWorkout(CardioExercise exercise)
        {
            bool check;
            string addCardio = "INSERT INTO cardio_exercise (exercise_id, duration, intensity, workout_id) VALUES (@exercise_id, @duration, @intensity, @workout_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(addCardio, conn);
                cmd.Parameters.AddWithValue("@exercise_id", exercise.ExerciseID);
                cmd.Parameters.AddWithValue("@duration", exercise.Duration);
                cmd.Parameters.AddWithValue("@intensity", exercise.Intensity);
                cmd.Parameters.AddWithValue("@workout_id", exercise.WorkoutID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public bool AddStrengthToWorkout(StrengthExercise exercise)
        {
            bool check;
            string addStrength = "INSERT INTO strength_exercises (exercise_id, strength_reps, strength_sets, rest_time, workout_id) VALUES (@exercise_id, @strength_reps, @rest_time, @workout)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(addStrength, conn);
                cmd.Parameters.AddWithValue("@exerciseID", exercise.ExerciseID);
                cmd.Parameters.AddWithValue("@strength_reps", exercise.Reps);
                cmd.Parameters.AddWithValue("@rest_sets", exercise.Sets);
                cmd.Parameters.AddWithValue("@rest_time", exercise.Rest_time);
                cmd.Parameters.AddWithValue("@workout_id", exercise.WorkoutID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            return check;
        }

        public bool CreatePlan(Plan insertPlan)
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

        public List<Exercise> GetExercisesForTrainer(int TrainerID)
        {
            List<Exercise> ExercisesByTrainer = new List<Exercise>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQLExercises = "SELECT exercise_id, exercise_name, exercise_description, video_link FROM exercises WHERE trainer_id = @trainer_id";

                using (SqlCommand cmd = new SqlCommand(SQLExercises, conn))
                {
                    cmd.Parameters.AddWithValue("@trainer_id", TrainerID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Exercise ExerciseToAdd = MapRowToExercise(reader);

                        ExercisesByTrainer.Add(ExerciseToAdd);
                    }
                }
            }

            return ExercisesByTrainer;

        }


        private Exercise MapRowToExercise(SqlDataReader reader)
        {
            return new Exercise()
            {
                Name = Convert.ToString(reader["exercise_name"]),
                Type = Convert.ToInt32(reader["exercise_type_id"]),
                Description = Convert.ToString(reader["exercise_description"]),
                ExerciseID = Convert.ToInt32(reader["exercise_id"]),
                VideoLink = Convert.ToString(reader["video_link"])
            };
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            return new User()
            {
                First_Name = Convert.ToString(reader["first_name"]),
                Last_Name = Convert.ToString(reader["last_name"]),
            };
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