using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class CMSController : Controller
    {
        cmsClass objCMS = new cmsClass();

        public PartialViewResult Error()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            ViewBag.Group = "Admin";
            var nav = objCMS.getPages();
            return View(nav);
        }

        public ActionResult Insert()
        {
            ViewBag.Group = "Admin";
            return View();
        }

        [HttpPost]
        public ActionResult Insert(navigation nav)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                nav.viewpath = "~/userPages/" + nav.name + ".html";
                nav.controller = "Editable";
                objCMS.createPage(nav);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Insert");
            }
        }

        public ActionResult Edit(int id)
        {
            var page = objCMS.getPage(id);
            return View(page);
        }

        [HttpPost]
        public ActionResult Edit(int _id, navigation page)
        {
            if (ModelState.IsValid)
            {
                objCMS.updatePage(page.id, page.name, page.group);
                return RedirectToAction("Index");
            }
            else
            {
                return Edit(_id);
            }
        }

        public ActionResult Delete(int _id)
        {
            var page = objCMS.getPage(_id);
            return View(page);
        }

        [HttpPost]
        public ActionResult Delete(navigation page)
        {
            objCMS.deletePage(page);
            return RedirectToAction("Index");
        }

    }
}
