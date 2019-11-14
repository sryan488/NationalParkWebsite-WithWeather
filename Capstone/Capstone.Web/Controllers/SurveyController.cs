using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {

        private IParkDAO parkDAO;
        public SurveyController(IParkDAO parkDAO)
        {
            this.parkDAO = parkDAO;
        }


        public IActionResult Index()
        {
            SurveyViewModel vm = new SurveyViewModel();
            IList<Park> Parks = parkDAO.GetParks();
            vm.ParkList = new SelectList(Parks, "ParkCode", "ParkName");

            var State = new List<SelectListItem> {
                    new SelectListItem { Text = "Alabama" },
                    new SelectListItem { Text = "Alaska" },
                    new SelectListItem { Text = "Arizona" },
                    new SelectListItem { Text = "Arkansas" },
                    new SelectListItem { Text = "California" },
                    new SelectListItem { Text = "Colorado" },
                    new SelectListItem { Text = "Connecticut" },
                    new SelectListItem { Text = "Delaware" },
                    new SelectListItem { Text = "Florida" },
                    new SelectListItem { Text = "Georgia" },
                    new SelectListItem { Text = "Hawaii" },
                    new SelectListItem { Text = "Idaho" },
                    new SelectListItem { Text = "Illinois" },
                    new SelectListItem { Text = "Indiana" },
                    new SelectListItem { Text = "Iowa" },
                    new SelectListItem { Text = "Kansas" },
                    new SelectListItem { Text = "Kentucky" },
                    new SelectListItem { Text = "Louisiana" },
                    new SelectListItem { Text = "Maine" },
                    new SelectListItem { Text = "Maryland" },
                    new SelectListItem { Text = "Massachusetts" },
                    new SelectListItem { Text = "Michigan" },
                    new SelectListItem { Text = "Minnesota" },
                    new SelectListItem { Text = "Mississippi" },
                    new SelectListItem { Text = "Missouri" },
                    new SelectListItem { Text = "Montana" },
                    new SelectListItem { Text = "North Carolina" },
                    new SelectListItem { Text = "North Dakota" },
                    new SelectListItem { Text = "Nebraska" },
                    new SelectListItem { Text = "Nevada" },
                    new SelectListItem { Text = "New Hampshire" },
                    new SelectListItem { Text = "New Jersey" },
                    new SelectListItem { Text = "New Mexico" },
                    new SelectListItem { Text = "New York" },
                    new SelectListItem { Text = "Ohio" },
                    new SelectListItem { Text = "Oklahoma" },
                    new SelectListItem { Text = "Oregon" },
                    new SelectListItem { Text = "Pennsylvania" },
                    new SelectListItem { Text = "Rhode Island" },
                    new SelectListItem { Text = "South Carolina" },
                    new SelectListItem { Text = "South Dakota" },
                    new SelectListItem { Text = "Tennessee" },
                    new SelectListItem { Text = "Texas" },
                    new SelectListItem { Text = "Utah" },
                    new SelectListItem { Text = "Vermont" },
                    new SelectListItem { Text = "Virginia" },
                    new SelectListItem { Text = "Washington" },
                    new SelectListItem { Text = "West Virginia" },
                    new SelectListItem { Text = "Wisconsin" },
                    new SelectListItem { Text = "Wyoming" }
                };

            vm.StateList = new SelectList(State, "Text", "Text");

            return View(vm);
        }

        [HttpPost]
        public IActionResult Submit(Survey post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            parkDAO.SavePost(post);
            
            return RedirectToAction("FavoriteParks");
        }

        [HttpGet]
        public IActionResult FavoriteParks()
        {
            IList<Survey> surveys = parkDAO.GetAllSurveys();
            return View(surveys);
        }
    }
}