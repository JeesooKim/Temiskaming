using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class AppointmentController : Controller 
    {
        //
        // GET: /Appointment/

        appointmentClass objApp = new appointmentClass();
        doctorClass objDoc = new doctorClass();

        public ActionResult Index(int i = 0)
        {
            if (i == 1)
            { ViewBag.Success = true; }
            else { ViewBag.Success = false; }

            var docList = objDoc.getDoctors();
            if (docList == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(docList);
            }

        }
 
        public ActionResult BookAppointment(int docID)
        {
            doctor doc = new doctor();
            doc = objDoc.getDoctorByID(docID);
            ViewBag.Name = doc.fname + " " + doc.lname;
            TempData["docID"] = docID;
            return View();
        }
        
        [HttpPost]
        public ActionResult BookAppointment(appointment newApp)
        {
            if (TempData["docID"] != null)
            {
                if(newApp.booking_date < DateTime.Now)
                {
                    ViewBag.Error = "Wrong date";
                    return View(); 
                }
                newApp.doctor_id = (int)TempData["docID"];
                if (ModelState.IsValid)
                {
                    try
                    {
                        objApp.commitInsert(newApp);
                        ViewBag.Success = true;
                        return RedirectToAction("Index", "Appointment", new { i = 1});
                    }
                    catch
                    {
                        return View();
                    }
                }
            }
            return View("Index");
        }

        [Authorize(Roles="Admin")]
        public ActionResult DoctorAppointments()
        {
            var docList = objDoc.getDoctors();
            if (docList == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(docList);
            }

        }
        
        public ActionResult AllAppointments(int id)
        {
            var allApp = objApp.getAllAppointmentsByID(id);

            return View(allApp);
        }

        public ActionResult DeleteAppointment(int id)
        {
            ViewBag.Group = "Admin";
            appointment loadApp = objApp.getAppointmentByID(id);
            if (loadApp == null)
            {
                return View();
            }
            else
            {
                TempData["id"] = id;
                return View(loadApp);
            }
         
        }

        [HttpPost]
        public ActionResult ConfirmDeleteAppointment()
        {
            int id = (int)TempData["id"];
            ViewBag.Group = "Admin";
            if (id > 0)
            {
                try
                {
                    objApp.commitDelete(id);
                    return RedirectToAction("DoctorAppointments");
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

