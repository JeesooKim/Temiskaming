using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class WaittimeController : Controller
    {
        WaittimeClass objWaittime = new WaittimeClass();

        public ActionResult Index()
        {
            List<SelectListItem> patientNumList = new List<SelectListItem>();//This creates a list of number for checking wait time of the last 5, 10, 15 patients.
            patientNumList.Add(new SelectListItem
            {Text = "5", Value = "5"});
            patientNumList.Add(new SelectListItem
            {Text = "10", Value = "10"});
            patientNumList.Add(new SelectListItem
            {Text = "15", Value = "15"});
            ViewBag.numList = patientNumList; //create a viewbag of list to pass to the view
            ViewBag.Wait_Time = Math.Round(objWaittime.GetWaitTime(5)).ToString(); //round the number to zero decimal palce to convert it to a string
            //By default, it displays the wait time for the last 5 patients.
            ViewBag.waitingPatients = objWaittime.GetWaitingPatients().ToString(); //Get the number of patients waiting
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<SelectListItem> patientNumList = new List<SelectListItem>();
            patientNumList.Add(new SelectListItem { Text = "5", Value = "5" });
            patientNumList.Add(new SelectListItem { Text = "10", Value = "10" });
            patientNumList.Add(new SelectListItem { Text = "15", Value = "15" });
            ViewBag.numList = patientNumList;
            int numPatient = Int32.Parse(form["NumPatients"]);
            ViewBag.PatientNumber = numPatient;
            ViewBag.Wait_Time = Math.Round(objWaittime.GetWaitTime(numPatient), 0).ToString(); //Pass the number of last patients to the function GetWaitTime to get the approximate wait time.
            ViewBag.waitingPatients = objWaittime.GetWaitingPatients().ToString();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult admin()
        {
            ViewBag.Group = "Admin";
            var patients = objWaittime.GetPatients(); //Gets all the patients' info
            return View(patients);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult update(int id)
        {
            ViewBag.Group = "Admin";
            var patient = objWaittime.GetPatientByID(id); //display a patient's info
            return View(patient);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult update(int id, waittime patient)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objWaittime.PatientUpdate(id, patient.name, patient.time_admit, patient.time_doctor); //update the patient's info
                    return RedirectToAction("admin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult delete(int id)
        {
            ViewBag.Group = "Admin";
            var patient = objWaittime.GetPatientByID(id);
            if (patient == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(patient);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult delete(int id, waittime patient)
        {
            ViewBag.Group = "Admin";
            try
            {
                objWaittime.PatientDelete(id);
                return RedirectToAction("admin");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult create()
        {
            ViewBag.Group = "Admin";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult create(waittime patient)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objWaittime.PatientInsert(patient);
                    return RedirectToAction("admin");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NotFound()
        {
            ViewBag.Group = "Admin";
            return View();
        }

    }
}
