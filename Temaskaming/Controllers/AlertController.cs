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
                return View(oneAlert);
            }

        }

        public ActionResult AddAlert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAlert(alert newAlert)
        {
            newAlert.alertStatus = false;

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

        public ActionResult UpdateAlert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAlert(int id, alert updAlert)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    alertObj.commitUpdateAlert(id, updAlert.alertTitle, updAlert.alertLevel, updAlert.alertDescription, (DateTime)updAlert.alertTimeline, (bool)updAlert.alertStatus);
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
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAlert(int id)
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
    }
}
