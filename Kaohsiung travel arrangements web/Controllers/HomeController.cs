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

        /*
         * 未完成：
         * - Google Map標示
         * - 景點介紹：景點依照地區分類
         * - 安排行程：顯示已經完成之參數
         * - 顯示結果：更換遊玩景點
         */

        /*
         * 人文：Humanities
         * 人造景觀：Artificial landscape
         * 休閒：Leisure
         * 自然：Natural
         * 宗教：Religion
        */
        public ActionResult ScheduleResult(string startDate, string endDate, int startWeek, int days, int hum_score, int art_score, int lei_score, int nat_score, int rel_score)
        {
            Session["startDate"] = startDate;
            Session["endDate"] = endDate;
            Session["startWeek"] = startWeek;
            Session["days"] = days;
            Session["hum_score"] = hum_score;
            Session["art_score"] = art_score;
            Session["lei_score"] = lei_score;
            Session["nat_score"] = nat_score;
            Session["rel_score"] = rel_score;

            return View();
        }

        public string ArrangeAttractions()
        {
            int startWeek = (int)Session["startWeek"];
            int days = (int)Session["days"];

            List<string[]> data = new List<string[]>();
            List<Calendar> cals = JsonConvert.DeserializeObject<List<Calendar>>(System.IO.File.ReadAllText(Server.MapPath("~/App_Data/attractions-opentime.json")));
            string[] source = System.IO.File.ReadAllLines(Server.MapPath("~/App_Data/attractions-all-information.csv"));
            for (int i = 1; i < source.Length; ++i)
                data.Add(source[i].Split(new char[] { ',' }));
            Attraction[] attrs = new Attraction[data.Count];
            TravelTime[][] travelTimes = new TravelTime[days][];

            

            for (int i = 0; i < attrs.Length; ++i)
            {
                Attraction attr = new Attraction();
                attr.cal = cals[i];
                attr.id = data[i][0];
                attr.name = data[i][1];
                attr.lon = Convert.ToDouble(data[i][7]);
                attr.lat = Convert.ToDouble(data[i][8]);
                attr.score = Convert.ToDouble(data[i][12]) * (int)Session[CategoryArgsName(data[i][11])];
                attrs[i] = attr;
            }

            Array.Sort(attrs);
            Random rand = new Random();
            for (int i = 0, week = startWeek; i < travelTimes.Length; ++i, ++week)
            {
                if (week > 7) week -= 7;
                
                int timePeriodCount = rand.Next(3, 5);
                travelTimes[i] = new TravelTime[timePeriodCount];

                if (timePeriodCount == 3)
                {
                    
                    travelTimes[i][0] = new TravelTime(week, "09:00", "11:00"); //早上行程
                    travelTimes[i][1] = new TravelTime(week, "13:00", "17:00"); //下午行程
                    travelTimes[i][2] = new TravelTime(week, "19:00", "22:00"); //晚上行程
                }
                else if (timePeriodCount == 4)
                {
                    travelTimes[i][0] = new TravelTime(week, "09:00", "11:00"); //早上行程
                    travelTimes[i][1] = new TravelTime(week, "13:00", "15:00"); //下午行程1
                    travelTimes[i][2] = new TravelTime(week, "15:00", "17:00"); //下午行程2
                    travelTimes[i][3] = new TravelTime(week, "19:00", "22:00"); //晚上行程
                }
            }

            MDTRA mdtra = new MDTRA(attrs, travelTimes);
            Arrangement[][] arrs = mdtra.arrangeAttractions();
            return JsonConvert.SerializeObject(arrs);
        }

        private string CategoryArgsName(string category)
        {
            if (category == "人文") return "hum_score";
            if (category == "人造景觀") return "art_score";
            if (category == "休閒") return "lei_score";
            if (category == "自然") return "nat_score";
            if (category == "宗教") return "rel_score";
            return "";
        }

        public string ArrangeDates()
        {
            string startDate = (string)Session["startDate"];
            string endDate = (string)Session["endDate"];
            DateTime startDateTime = DateTime.Parse(startDate);
            DateTime endDateTime = DateTime.Parse(endDate);
            TimeSpan day1 = new TimeSpan(1,0,0,0);
            List<string> dates = new List<string>();

            dates.Add(startDateTime.ToString("MM/dd"));
            while (startDateTime < endDateTime)
            {
                startDateTime = startDateTime + day1;
                dates.Add(startDateTime.ToString("MM/dd"));
            }
            return JsonConvert.SerializeObject(dates);
        }

        public ActionResult AttractionIntroduction()
        {
            return View();
        }

        public string AttractionsInfo()
        {
            List<string[]> data = new List<string[]>();
            string[] source = System.IO.File.ReadAllLines(Server.MapPath("~/App_Data/attractions-all-information.csv"));
            for(int i=1; i<source.Length; ++i)
            {
                data.Add(source[i].Split(new char[] {','}));
            }

            return JsonConvert.SerializeObject(data);
        }

        public string AttractionsCal()
        {
            return System.IO.File.ReadAllText(Server.MapPath("~/App_Data/attractions-opentime.json"));
        }

        [HttpPost]
        public string AttractionReplacement(int week, string startTime, string endTime)
        {
            List<Attraction> attrs = new List<Attraction>();
            List<string[]> data = new List<string[]>();
            string[] source = System.IO.File.ReadAllLines(Server.MapPath("~/App_Data/attractions-all-information.csv"));
            for (int i = 1; i < source.Length; ++i)
            {
                data.Add(source[i].Split(new char[] { ',' }));
            }

            List<Calendar> cals = JsonConvert.DeserializeObject<List<Calendar>>(System.IO.File.ReadAllText(Server.MapPath("~/App_Data/attractions-opentime.json")));
            for(int i=0; i<data.Count; ++i)
            {
                if (!TimeIntersection(cals[i], week, startTime, endTime)) continue;
                string[] line = data[i];
                Attraction attr = new Attraction();
                attr.id = line[0];
                attr.name = line[1];
                attr.lon = Convert.ToDouble(line[7]);
                attr.lat = Convert.ToDouble(line[8]);
                attr.score = Convert.ToDouble(line[12]) * (int)Session[CategoryArgsName(line[11])];
                attr.cal = cals[i];

                attrs.Add(attr);
            }
            return JsonConvert.SerializeObject(attrs);
        }

        private bool TimeIntersection(Calendar cal, int week, string startTime, string endTime)
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