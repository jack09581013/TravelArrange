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
            for(int i=1; i<source.Length; ++i)
            {
                data.Add(source[i].Split(new char[] {','}));
            }

            return JsonConvert.SerializeObject(data);
        }

        [HttpPost]
        public string AttractionReplacement(int week, string startTime, string endTime)
        {
            List<Attraction> attrs = new List<Attraction>();
            List<string[]> data = new List<string[]>();
            string[] source = System.IO.File.ReadAllLines(root + "/App_Data/attractions-all-information.csv");
            for (int i = 1; i < source.Length; ++i)
            {
                data.Add(source[i].Split(new char[] { ',' }));
            }

            List<Calendar> cals = JsonConvert.DeserializeObject<List<Calendar>>(System.IO.File.ReadAllText(root + "/App_Data/attractions-opentime.json"));
            for(int i=0; i<data.Count; ++i)
            {
                if (!timeIntersection(cals[i], week, startTime, endTime)) continue;
                string[] line = data[i];
                Attraction attr = new Attraction();
                attr.id = line[0];
                attr.name = line[1];
                attr.lon = Convert.ToDouble(line[6]);
                attr.lat = Convert.ToDouble(line[7]);
                attr.score = Convert.ToDouble(line[9]);
                attr.cal = cals[i];

                attrs.Add(attr);
            }
            return JsonConvert.SerializeObject(attrs);
        }

        private bool timeIntersection(Calendar cal, int week, string startTime, string endTime)
        {
            List<string[]> opentimes = cal.calendar[week];
            if (cal.attribute == "all") return true;
            else if (cal.attribute == "part")
            {
                foreach (string[] opentime in opentimes)
                    if (string.Compare(opentime[0], endTime) < 0 && string.Compare(startTime, opentime[1]) < 0)
                        return true;
                return false;
            }
            else if (cal.attribute == "none")
            {
                return false;
            }
            else throw new Exception("Calendar attribute error: " + cal.attribute);
        }

    }
}