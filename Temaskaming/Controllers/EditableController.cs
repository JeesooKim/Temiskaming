using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class EditableController : Controller
    {
        cmsClass objCMS = new cmsClass();

        public ActionResult Index(int id)
        {
            var page = objCMS.getPage(id);
            ViewBag.Title = "Temiskaming - " + page.name;
            ViewBag.Header = page.name;
            ViewBag.Group = page.group;
            ViewBag.Content = page.viewpath.ToString();
            return View();
        }

    }
}
