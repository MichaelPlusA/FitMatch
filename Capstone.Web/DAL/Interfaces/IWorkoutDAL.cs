using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IWorkoutDAL
    {
        bool AddExercise(string name, string description, int id);

        List<Plan> GetPlans(int traineeID);
    }
}