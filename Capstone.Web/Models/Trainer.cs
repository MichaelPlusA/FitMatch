using Capstone.Web.Helpers;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Trainer : User
    {

        private const int SALT_LENGTH = 12;

        public int Rating { get; set; } //not part of MVP, for future use. Scale of 1-5
        public string Additional_notes { get; set; } //Lengthy
        public double PricePerHour { get; set; }
        public int YearsExp { get; set; }
        public string Philosophy { get; set; }
        public string ClientSuccessStories { get; set; }
        public string Certifications { get; set; }

        public Trainer(RegisterViewModel user)
        {
            First_Name = user.First_Name;
            Last_Name = user.Last_Name;
            this.Email = user.Email;
            Additional_notes = user.Additional_notes;
            PricePerHour = user.PricePerHour;
            YearsExp = user.YearsExp;
            Philosophy = user.Philosophy;
            ClientSuccessStories = user.ClientSuccessStories;
            Certifications = user.Certifications;

            byte[] saltString = Security.GenerateSalt(SALT_LENGTH);

            Salt = Convert.ToBase64String(saltString);

            Password = Security.Hash(user.Password, saltString);
        }

    }
}