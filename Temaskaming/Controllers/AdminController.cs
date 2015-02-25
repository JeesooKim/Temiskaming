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

        public ActionResult Index(bool? logout)
        {
            if (Session["ID"] != null && logout != true)
            {
                return View("Welcome");
            } 
            else if(logout == true)
            {
                Session.Abandon();
                ViewBag.Message = "You are logged out.";
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Welcome()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult Welcome(admin admin)
        {
            if (ModelState.IsValid)
            {
                int id;
                try {
                    id = objAdmin.getAdminID(admin.login, admin.pass);
                }
                catch
                {
                    id = -1;
                }
                
                if (id != -1)
                {
                    Session["ID"] = id.ToString();
                    Session["USER"] = admin.login;
                    return View();
                }
                else
                {
                    ViewBag.Error = "Login or Password supplied were incorrect";
                    return View("Index");
                }
                
            }
            else
            {
                return View("Index");
            }
        }

    }
}
