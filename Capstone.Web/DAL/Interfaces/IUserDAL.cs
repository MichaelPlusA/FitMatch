using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IUserDAL
    {
        bool RegisterUser(User newUser);
        User GetCurrentUser(string email);
        bool RegisterUser(Trainer trainMaster);
        Trainer GetTrainer(int? ID);
    }
}
