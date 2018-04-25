using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Trainer : User
    {
        public int Rating { get; set; } //not part of MVP, for future use. Scale of 1-5
        public string Additional_notes { get; set; } //Lengthy
        public double PricePerHour { get; set; }
        public int YearsExp { get; set; }
        public string Philosophy { get; set; }
        public string ClientSuccessStories { get; set; }
        public List<string> Certifications { get; set; }
    }
}