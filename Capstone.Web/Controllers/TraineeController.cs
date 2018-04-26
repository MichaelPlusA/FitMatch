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

        public TraineeController(IProfileDAL dal)
        {
            _dal = dal;
        }

        // GET: Trainee
        public ActionResult Index()
        {
            return View();
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
    }
}