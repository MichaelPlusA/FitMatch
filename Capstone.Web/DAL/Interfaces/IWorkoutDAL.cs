using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IWorkoutDAL
    {
        bool AddExercise(Exercise addExercise);
        List<Plan> GetPlans(int traineeID);
        bool CreatePlan(Plan insertPlan);
        PopulatePlanViewModel GetPlanViewModel(int traineeID);
        List<Exercise> GetExercisesForTrainer(int TrainerID);
        List<Workout> GetWorkouts(int planID);
    }
}