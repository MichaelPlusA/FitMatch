using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
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
                trainerLogin.ClientList = _dal.GetClients((int)trainerLogin.Trainer_ID);
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

        public ActionResult ClientServices()
        {
            Trainer loggedInTrainer = new Trainer();

            int trainerID = ((int)Session[SessionKeys.Trainer_ID]);
            loggedInTrainer.ClientList = _dal.GetClients(trainerID);
            return View(loggedInTrainer.ClientList);
        }

        public ActionResult Detail(int id)
        {

            Exercise exercise = _dal.GetExercise(id);
            return View("Detail", exercise);
        }

        [HttpPost]
        public ActionResult CreatePlan(Plan CreatePlan)
        {
            int trainerID = ((int)Session[SessionKeys.Trainer_ID]);
            CreatePlan.ByTrainer = ((int)Session[SessionKeys.Trainer_ID]);
            int planId = _workoutDal.CreatePlan(CreatePlan);

            return RedirectToAction("CreateWorkout", new { planId = planId });
        }

        public ActionResult CreateWorkout(int planId)
        {
            Plan plan = _workoutDal.GetPlan(planId);
            plan.SeveralWorkouts = _workoutDal.GetWorkouts(planId);
            return View(plan);
        }

        [HttpPost]
        public ActionResult CreateWorkout(string name, string notes, int planID)
        {
            bool isAdded = _workoutDal.CreateWorkout(name, notes, planID);

            return RedirectToAction("CreateWorkout", new { planId = planID });
        }

        public ActionResult AddExercisesToWorkout(int planId, int workoutId)
        {
            int trainerId = (int)Session[SessionKeys.Trainer_ID];

            PopulatePlanViewModel vm = new PopulatePlanViewModel();
            vm.Exercises = _workoutDal.GetExercisesForTrainer(trainerId);
            vm.Strength = _workoutDal.GetStrengthExercises(workoutId);
            vm.Cardio = _workoutDal.GetCardioExercises(workoutId);

            return View(vm);
        }

        [HttpPost]
        public ActionResult SubmitExercise(Exercise addExercise)
        {
            string[] splitLink = addExercise.VideoLink.Split('=');
            addExercise.VideoLink = "https://www.youtube.com/embed/" + splitLink[1];
            addExercise.TrainerID = ((int)Session[SessionKeys.Trainer_ID]);
            bool AddExercise = _workoutDal.AddExercise(addExercise);

            return Redirect("/Trainer/Index");
        }
    }
}