using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Kaohsiung_travel_arrangements_web.Controllers
{
    
    public class HomeController : Controller
    {
        private string root = "C:/Users/USER/Documents/Visual Studio 2015/Projects/TravelArrange/Kaohsiung travel arrangements web";
        // GET: Home
        public ActionResult SchedulePlanning()
        {            
            return View();
        }

        public ActionResult ScheduleResult()
        {
            return View();
        }

        public ActionResult AttractionIntroduction()
        {
            return View();
        }

        public string AttractionsInfo()
        {
            List<string[]> data = new List<string[]>();
            string[] source = System.IO.File.ReadAllLines(root + "/App_Data/attractions-all-information.csv");
            foreach(string line in source)
            {
                data.Add(line.Split(new char[] {','}));
            }

            return JsonConvert.SerializeObject(data);
        }
    }
}