using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaohsiung_travel_arrangements_web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult SchedulePlanning()
        {
            MDTRA mdtra;
            return View();
        }

        public ActionResult ScheduleResult()
        {
            return View();
        }
    }
}