﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Http;


namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO;
        public HomeController(IParkDAO parkDAO)
        {
            this.parkDAO = parkDAO;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<Park> parks = parkDAO.GetParks();
            return View(parks);
        }

        public IActionResult Detail(Park park)
        {
            string tempScale = HttpContext.Session.GetString("tempScale");
            if (tempScale == null)
            {
                tempScale = "F";
                HttpContext.Session.SetString("tempScale", tempScale);
            }
            park = parkDAO.GetPark(park.ParkCode);
            IList<Weather> weatherList = parkDAO.GetWeather(park.ParkCode);
            park.WeatherList = weatherList;
            park.TempScale = tempScale;

            Dictionary<string, string> weatherPrep = new Dictionary<string, string>()
            {
                {"sunny", "Pack sunblock" },
                {"partly cloudy",  "" },
                {"cloudy", "" },
                {"rain", "Pack Rain gear and wear waterproof shoes"},
                {"thunderstorms", "Seek shelter and avoid hikin on exposed ridges"},
                {"snow", "Pack snowshoes" }
            };
            park.WeatherPrep = weatherPrep;

            return View(park);
        }

        [HttpPost]
        public IActionResult Detail(String parkCode, string tempScale)
        {
            HttpContext.Session.SetString("tempScale", tempScale);
            return RedirectToAction("Detail", new { parkCode = parkCode });
        }
        public IActionResult ChooseTempScale(Park TempScale, string code)
        {
            return RedirectToAction("Detail", new { @code = code });
        }


        private const string tempSessionKey = "preferredTemp";

        protected string GetPreferredTempScale()
        {
            return HttpContext.Session.GetString(tempSessionKey) ?? "";
        }

        protected void SetPreferredTempScale(string tempScale)
        {
            if (tempScale == null || tempScale.Trim().Length == 0)
            {
                ClearPreferredTempScale();
            }
            else
            {
                HttpContext.Session.SetString(tempSessionKey, "celcius");
            }
        }

        protected void ClearPreferredTempScale()
        {
            HttpContext.Session.Remove(tempSessionKey);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
