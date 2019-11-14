using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IParkDAO
    {
        IList<Park> GetParks();
        Park GetPark(string code);
        int SavePost(Survey newSurvey);
        IList<Survey> GetAllSurveys();
        IList<Weather> GetWeather(string parkCode);

    }
}
