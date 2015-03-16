using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        NewsClass objNews = new NewsClass();
        public ActionResult Index()
        {
            var news = objNews.getNews();
            return View(news);
        }

        public ActionResult newsadmin()
        {
            var news = objNews.getNews();
            return View(news);
        }

        public ActionResult newsupdate()
        {
            return View();
        }

        public ActionResult newsdelete()
        {
            return View();
        }

        public ActionResult newsinsert()
        {
            return View();
        }
    }
}
