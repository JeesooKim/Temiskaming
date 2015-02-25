using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class HomeController : Controller
    {
        navClass objNav = new navClass();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Nav(string group)
        {
            var nav = objNav.getNav(group);
            return PartialView(nav);
        }

        public ActionResult Patients()
        {
            return View();
        }

        public ActionResult ProgramsServices()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult JoinOurTeam()
        {
            return View();
        }


        public ActionResult ContactUs()
        {
            return View();
        }

    }
}
