using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Trainer : User
    {
        public int Rating { get; set; } //not part of MVP, for future use. Scale of 1-5
        public string Bio { get; set; } //Lengthy
        public double PricePerHour { get; set; }
        public int YearsExp { get; set; }
        public string Philosophy { get; set; }
        public string Background { get; set; } //Brief - Ex.) Former NBA Player
        public List<string> Certifications { get; set; }
    }
}