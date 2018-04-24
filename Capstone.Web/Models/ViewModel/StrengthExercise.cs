using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModel
{
    public class StrengthExercise : Exercise
    {
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}