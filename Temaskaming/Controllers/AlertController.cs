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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()                         // get : admin page, shows a list of all alerts
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

        [Authorize(Roles = "Admin")]                        // get : admin page, shows that alert that has been posted
        public ActionResult ActiveAlert()                   // only one alert can be active at a time
        {
            var oneAlert = alertObj.getActiveAlert();
            if (oneAlert == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView(oneAlert);               // returns a partial view, so that it can be renedered in the public page 
            }                                               // to be displayed on the right side column
                                                                
        }

        [Authorize(Roles = "Admin")]                        // get : admin page
        public ActionResult PublishAlert(int id)            // changes the value of the publish column to true for a particular id
        {                                                   // any alert which is already published, value is set to false.
            ViewBag.Group = "Admin";
            bool result = alertObj.publishAlert(id);
            if (result == false)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddAlert()          // get : admin page, shows a form to add an alert and pick a date 
        {
            ViewBag.Group = "Admin";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddAlert(alert newAlert)        // post : admin page, form values are insertrd in the database
        {                                                   // the alert status by default is set to false
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

        [Authorize(Roles = "Admin")]                
        public ActionResult UpdateAlert(int id)                      // get: admin page
        {                                                            //  shows a form to update a alert
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

        [Authorize(Roles = "Admin")]                                // post : admin page
        [HttpPost]                                                                      
        public ActionResult UpdateAlert(int id, alert updAlert)       // form values are sent to db to be updated
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

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAlert(int id)                 // get : admin page
        {                                                       // shows the record that has been requested to be deleted
            ViewBag.Group = "Admin";
            alert loadAlert = alertObj.getAlertByID(id);
            if (loadAlert == null)
            {
                return View();
            }
            else
            {
                TempData["id"] = id;
                return View(loadAlert);
            }
         
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ConfirmDeleteAlert()        // post : admin page
        {                                               // when admin confirms the deletion, record is deleted from db
            int id = (int)TempData["id"];
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
