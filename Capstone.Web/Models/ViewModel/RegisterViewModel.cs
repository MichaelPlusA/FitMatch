using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Capstone.Web.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}