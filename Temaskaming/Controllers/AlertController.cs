using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class AlertController : Controller
    {
        // properties
        alertClass alertObj = new alertClass();

        //
        // GET: /Alert/

        public ActionResult Index()
        {
            ViewBag.Group = "Admin";
            var allAlerts = alertObj.getAllAlerts();
            if (allAlerts == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(allAlerts);
            }

        }

        public ActionResult ActiveAlert()
        {
            var oneAlert = alertObj.getActiveAlert();
            if (oneAlert == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView(oneAlert);
            }

        }

        public ActionResult AddAlert()
        {
            ViewBag.Group = "Admin";
            return View();
        }

        [HttpPost]
        public ActionResult AddAlert(alert newAlert)
        {
            newAlert.alertStatus = false;
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    alertObj.commitInsertAlert(newAlert);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult UpdateAlert(int id)
        {
            ViewBag.Group = "Admin";
            alert loadAlert = alertObj.getAlertByID(id);
            if (loadAlert == null)
            { 
                return View(); 
            }
            else
            {
                return View(loadAlert);
            }
        }

        [HttpPost]
        public ActionResult UpdateAlert(int id, alert updAlert)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    alertObj.commitUpdateAlert(id, updAlert.alertTitle, updAlert.alertLevel, updAlert.alertDescription, (DateTime)updAlert.alertTimeline);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult DeleteAlert()
        {
            ViewBag.Group = "Admin";
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAlert(int id)
        {
            ViewBag.Group = "Admin";
            if (id > 0)
            {
                try
                {
                    alertObj.commitDeleteAlert(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }  
}
