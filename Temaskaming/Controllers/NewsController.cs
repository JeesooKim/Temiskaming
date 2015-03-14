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
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult newsadmin()
        {
            return View();
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
