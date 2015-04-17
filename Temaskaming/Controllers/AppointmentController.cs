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

        public ActionResult Index(int i = 0)                  // get : public page, displays a list of doctors to book an appointment with
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
 
        public ActionResult BookAppointment(int docID)  // get : public page ,  shows a form to fill in the contact details 
        {
            doctor doc = new doctor();
            doc = objDoc.getDoctorByID(docID);
            ViewBag.Name = doc.fname + " " + doc.lname;
            TempData["docID"] = docID;                              // shows the doctor who user chose for appointment
            return View();
        }
        
        [HttpPost]
        public ActionResult BookAppointment(appointment newApp)   // post : public page, submits the details entered by the user
        {
            if (TempData["docID"] != null)
            {
                if(newApp.booking_date < DateTime.Now)              // throws an error when you choose a date which is less than the 
                {                                                   // the current date
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
                        return RedirectToAction("Index", "Appointment", new { i = 1});   // redirects with a query string i=1
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
        public ActionResult DoctorAppointments()   // get : admin page, displays a list of doctors and an option to see their appointments
        {
            ViewBag.Group = "Admin";

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

        [Authorize(Roles = "Admin")]
        public ActionResult AllAppointments(int id)        // get : admin page, shows a particular doctor with a list of his appointments 
        {

            ViewBag.Group = "Admin";
            var allApp = objApp.getAllAppointmentsByID(id);

            return View(allApp);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAppointment(int id)       // get : admin page, admin can only cancel appointments
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
                return View(loadApp);                    // loads appointment that needs to be cnaceled.
            }
         
        }

        [Authorize(Roles = "Admin")]                    // post : admin page, admin confirms to cancel an appointment
        [HttpPost]
        public ActionResult ConfirmDeleteAppointment()
        {
            ViewBag.Group = "Admin";
            int id = (int)TempData["id"];
            ViewBag.Group = "Admin";
            if (id > 0)
            {
                try
                {
                    objApp.commitDelete(id);                            // appointment is canceled from database.
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

