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
            ViewBag.LastId = objCMS.getLastId() + 1;
            return View();
            
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Insert(string content, string group, navigation nav)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                nav.viewpath = nav.name;
                if (group != "None")
                {
                    nav.group = group;
                }
                nav.controller = "Editable";
                string path = Server.MapPath("~/userPages/" + nav.name + ".html");
                
                objCMS.createPage(path, content, nav);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Insert");
            }
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Group = "Admin";
            var page = objCMS.getPage(Id);
            ViewBag.CurrId = page.id;
            return View(page);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, string content, string name, string group)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/userPages/" + name + ".html");
                objCMS.updatePage(path, content, id, name, group);
                return RedirectToAction("Index");
            }
            else
            {
                return Edit(id);
            }
        }

        public ActionResult Delete(int Id)
        {
            ViewBag.Group = "Admin";
            var page = objCMS.getPage(Id);
            return View(page);
        }

        [HttpPost]
        public ActionResult Delete(string action, int id, string name)
        {
            ViewBag.Group = "Admin";
            if (action == "Yes")
            {
                string path = Server.MapPath("~/userPages/" + name + ".html");
                objCMS.deletePage(path, id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
