using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IProfileDAL
    {
        List<User> TrainerProfileSearchName(string trainerFirstName, string trainerLastName);
        List<User> TrainerProfileSearchPrice(int pricePerHour);
    }
}
