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
        private IUserDAL _dal;

        public UserController(IUserDAL dal)
        {
            _dal = dal;
        }
        

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
                //model.PlansList = _dalWorkout.GetPlan(model.UserID);
                return View("Login", model);
            }

            User user = _dal.GetCurrentUser(model.Email);
            bool isValidPassword = user.isValidPassword(model.Password);

            //if user does not exist or password is wrong
            if(user == null || !isValidPassword)
            {
                ModelState.AddModelError("invalid credentials", "An invalid username or password was entered");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                Session[SessionKeys.Email] = user.Email;
                Session[SessionKeys.UserID] = user.User_ID;
                Session[SessionKeys.Trainer_ID] = user.Trainer_ID;
                Session[SessionKeys.First_Name] = user.First_Name;
            }

            if(user.Trainer_ID != null)
            {
                
                return RedirectToAction("Index", "Trainer");      
            }

            
            return RedirectToAction("Index", "Trainee");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            if(user.Is_trainer)
            {
                Trainer newtrainer = new Trainer(user);
                bool isAdded = _dal.RegisterUser(newtrainer);

                if(isAdded)
                {
                    LoginViewModel loginVM = new LoginViewModel()
                    {
                        Email = newtrainer.Email,
                        Password = newtrainer.Password
                    };

                    // TODO: redirect to logged in home page
                    return Login(loginVM);
                }
            } 
            else
            {
                User newUser = new User(user);
                bool isAdded = _dal.RegisterUser(newUser);

                if (isAdded)
                {
                    LoginToAccount(newUser);
                }
            }

            return View("Register");
        }

        private ActionResult LoginToAccount(User user)
        {
            LoginViewModel loginVM = new LoginViewModel()
            {
                Email = user.Email,
                Password = user.Password,
            };

            // TODO: redirect to logged in home page
            return Login(loginVM);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.First_Name);
            Session.Remove(SessionKeys.Email);
            Session.Remove(SessionKeys.Trainer_ID);
            Session.Remove(SessionKeys.UserID);

            return RedirectToAction("Index", "Home");
        }
    }
}