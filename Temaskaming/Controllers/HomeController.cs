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
            NewsClass objNews = new NewsClass();
            var news = objNews.getLatestNews();
            return View(news);
        }

        [Authorize]
        public ActionResult Admin()
        {
            if (User.IsInRole("Nurse"))
            {
                ViewBag.Group = "Nurse";
            }
            else
            {
                ViewBag.Group = "Admin";
            }
            
            return View();
        }

        public PartialViewResult Nav(string group, string chosen)
        {
            if (chosen != null)
            {
                ViewBag.Chosen = chosen;
            }
            var nav = objNav.getNav(group);
            return PartialView(nav);
        }

        public ActionResult Patients()
        {
            ViewBag.Group = "Patients";
            return View();
        }

        public ActionResult ProgramsServices()
        {
            ViewBag.Group = "ProgramsServices";
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Group = "AboutUs";
            return View();
        }

        public ActionResult JoinOurTeam()
        {
            ViewBag.Group = "JoinOurTeam";
            return View();
        }


        public ActionResult ContactUs()
        {
            ViewBag.Group = "ContactUs";
            return View();
        }

    }
}
