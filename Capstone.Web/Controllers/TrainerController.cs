using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class TrainerController : Controller
    {
        private IUserDAL _dal;
        private IWorkoutDAL _workoutDal;

        public TrainerController(IUserDAL dal, IWorkoutDAL workoutDal)
        {
            _dal = dal;
            _workoutDal = workoutDal;
        }

        // GET: Trainer
        public ActionResult Index()
        {
            if(Session[SessionKeys.Trainer_ID] != null)
            {
                Trainer trainerLogin = _dal.GetTrainer((int)Session[SessionKeys.Trainer_ID]);
                return View(trainerLogin);
            }

            return Redirect("/User/Login");
        }

        public ActionResult EditTrainer()
        {
            if (Session[SessionKeys.Trainer_ID] != null)
            {
                Trainer EditTrainer = _dal.GetTrainer((int)Session[SessionKeys.Trainer_ID]);
                int placeholder = ((int)Session[SessionKeys.Trainer_ID]);
                return View("EditTrainer", EditTrainer);
            }

            return Redirect("/User/Login");
        }

        public ActionResult SuccessfulEdit(Trainer edited)
        {
            edited.Trainer_ID = ((int)Session[SessionKeys.Trainer_ID]);
            edited.User_ID = ((int)Session[SessionKeys.UserID]);
            bool EditTrainer = _dal.UpdateTrainer(edited);
            return Redirect("/Trainer/Index");
        }

        public ActionResult ChangeAccess(string access)
        {
            int trainerID = ((int)Session[SessionKeys.Trainer_ID]);
            bool TrainerAccess = _dal.SwitchAccess(trainerID, access);
            return Redirect("/Trainer/Index");
        }

        public ActionResult AddExercise()
        {
            return View();
        }

        public ActionResult Requests()
        {
            return View();
        }

        public ActionResult TrainerMessages()
        {
            return View();
        }

        public ActionResult SubmitExercise(string name, string description)
        {
            int trainerID = ((int)Session[SessionKeys.Trainer_ID]);
            bool addExercise = _workoutDal.AddExercise(name, description, trainerID);

            return Redirect("/Trainer/Index");
        }
    }
}