using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class TraineeController : Controller
    {
        private IProfileDAL _dal;

        private IWorkoutDAL _dalWorkout;

        public TraineeController(IProfileDAL dal, IWorkoutDAL dalWork)
        {
            _dal = dal;
            _dalWorkout = dalWork;
        }

        // GET: Trainee
        public ActionResult Index()
        {
            int ID = (int)Session[SessionKeys.UserID];

            List<Plan> plans = _dalWorkout.GetPlans(ID);

            return View(plans);
        }

        public ActionResult Search()
        {
            return View("Search");
        }

        [HttpGet]
        public ActionResult SearchResult(string firstName, string lastName, double price)
        {
            List<User> users = null;

            if (!string.IsNullOrEmpty(lastName))
            {
                //users = _dal.TrainerProfileSearchName();
            }
            return View("Search");
        }
    }
}