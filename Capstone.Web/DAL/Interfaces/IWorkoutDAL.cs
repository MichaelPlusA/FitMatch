﻿using Capstone.Web.Models;
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
        List<User> GetClientNames(int TrainerID);

    }
}