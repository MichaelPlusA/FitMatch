using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Workout
    {
        public List<CardioExercise> RunningAndStuff { get; set; }
        public List<StrengthExercise> GetBig { get; set; }
    }
}