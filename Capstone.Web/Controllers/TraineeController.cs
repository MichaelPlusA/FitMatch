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

        private IUserDAL _dalUser;

        public TraineeController(IProfileDAL dal, IWorkoutDAL dalWork, IUserDAL dalUser)
        {
            _dal = dal;
            _dalWorkout = dalWork;
            _dalUser = dalUser;
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
            
            if(searchType.Equals("name"))
            {
                string[] names = searchString.Split();
                users = _dal.TrainerProfileSearchFullName(names[0], names[1]);
            }
            else if (searchType.Equals("price"))
            {
                int price = Convert.ToInt32(searchString);
                users = _dal.TrainerProfileSearchPrice(price);
            }

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrainerProfile(string id)
        {
            if(id == null)
            {
                return Redirect("/Trainee/Search");
            }

            Trainer Searchedtrainer = _dalUser.GetTrainer(Convert.ToInt32(id));
            return View("TrainerProfile", Searchedtrainer);
            
        }
    }
}