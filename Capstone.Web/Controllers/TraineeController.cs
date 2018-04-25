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

        public TraineeController(IProfileDAL dal)
        {
            _dal = dal;
        }

        // GET: Trainee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string firstName, string lastName, double price)
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