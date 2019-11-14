using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyViewModel
    {
        [Display(Name = "Favorite National Park")]
        public string ParkCode { get; set; }
        public SelectList ParkList { get; set; } 

        [Required(ErrorMessage = "Please supply a valid email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "State of Residence")]
        public string State { get; set; }
        public SelectList StateList { get; set; }

        [Display(Name = "Activity Level")]
        public string ActivityLevel { get; set; }
        public SelectList ActivityList { get; set; }

    }
}
