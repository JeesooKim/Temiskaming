using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class AdminController : Controller
    {
        adminClass objAdmin = new adminClass();

        public ActionResult Index()
        {
            return View();
        }

    }
}
