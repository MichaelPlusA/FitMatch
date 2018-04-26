using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Plan
    {
        public List<Workout> SeveralWorkouts { get; set; }
        public string PlanName { get; set; }
        public string Notes { get; set; }
        public string ForTrainee { get; set; } //who the workout plan is for
        public string ByTrainer { get; set; } //who assigned the workout plan
    }
}