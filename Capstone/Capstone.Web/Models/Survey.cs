using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string ParkCode { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string ActivityLevel { get; set; }
        public int Count { get; set; }
        public string ParkName { get; set; }

        public Survey() { }

        public Survey(int surveyId, string parkCode, string emailAddress, string state, string activityLevel, int count, string parkName)
        {
            SurveyId = surveyId;
            ParkCode = parkCode;
            EmailAddress = emailAddress;
            State = state;
            ActivityLevel = activityLevel;
            Count = count;
            ParkName = parkName;
        }
    }
}
