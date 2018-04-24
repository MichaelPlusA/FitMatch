using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserDAL userDal;

        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Login", model);
            }

            User user = userDal.GetCurrentUser(model.Email);

            //if user does not exist or password is wrong
            if(user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("invalid credentials", "An invalid username or password was entered");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                Session[SessionKeys.Email] = user.Email;
                Session[SessionKeys.UserID] = user.UserID;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}