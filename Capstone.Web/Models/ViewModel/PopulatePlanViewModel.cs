using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModel
{
    public class PopulatePlanViewModel
    {
        public List<Exercise> exercises { get; set; } = new List<Exercise>();
        public List<Workout> workouts { get; set; } = new List<Workout>();
        public int PlanID { get; set; }
        public string PlanName { get; set; }
    }
}