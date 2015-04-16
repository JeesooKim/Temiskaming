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

        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            ViewBag.Group = "Admin";
            var nav = objCMS.getPages();
            return View(nav);
        }

        [Authorize(Roles="Admin")]
        public ActionResult Insert()
        {
            ViewBag.Group = "Admin";
            ViewBag.LastId = objCMS.getLastId() + 1;
            ViewBag.GroupList = objCMS.allGroups();
            return View();
            
        }
        
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles="Admin")]
        public ActionResult Insert(string content, string group, navigation nav)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                nav.viewpath = nav.name;
                if (group != "")
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

        [Authorize(Roles="Admin")]
        public ActionResult Edit(int Id)
        {
            ViewBag.Group = "Admin";
            var page = objCMS.getPage(Id);
            ViewBag.CurrId = page.id;
            ViewBag.GroupList = objCMS.allGroups();
            ViewBag.SelGroup = page.group;
            return View(page);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles="Admin")]
        public ActionResult Edit(int id, string content, string name, string grouping)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/userPages/" + name + ".html");
                objCMS.updatePage(path, content, id, name, grouping);
                return RedirectToAction("Index");
            }
            else
            {
                return Edit(id);
            }
        }

        [Authorize(Roles="Admin")]
        public ActionResult Delete(int Id)
        {
            ViewBag.Group = "Admin";
            var page = objCMS.getPage(Id);
            return View(page);
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
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
