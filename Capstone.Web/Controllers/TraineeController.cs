using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

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

        public ActionResult Search(string json)
        {
            return View("Search", (object)json);
        }

        [HttpGet]
        public ActionResult SearchResult(string searchString, string searchType)
        {
            List<User> users = null;

            //if (!string.IsNullOrEmpty(lastName))
            //{
            //    //users = _dal.TrainerProfileSearchName();
            //}

            if (searchType.Equals("price"))
            {
                int price = Convert.ToInt32(searchString);
                users = _dal.TrainerProfileSearchPrice(price);
            }

            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}