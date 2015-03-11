using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace FindADoctor.Controllers
{
    public class DoctorsController : Controller
    {
        //
        // GET: /Doctors/
        doctorClass objDoc = new doctorClass();
        
        public ActionResult Details(int id)
        {
            ViewBag.Group = "ContactUs";
            var doc = objDoc.getDoctorByID(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(doc);
            }
        }



        // public ActionResult SearchIndex(string searchString)
        public ActionResult Index(string medicalDepartment, string searchString)
        {
            ViewBag.Group = "ContactUs";
            var DepartLst = new List<string>();
            //this creates a List object to hold medical departments from the database.
            //the following code is a LINQ query that retrieves all the medical departmentsfrom the db.
            var DepartQry = from m in objDoc.getDoctors()
                            orderby m.department
                            select m.department;
            DepartLst.AddRange(DepartQry.Distinct());
            //AddRange: the method of the generic List collection to add all the distinct medical departments to the list. Without the Distinct modifier, duplicate departments would be added.

            ViewBag.medicalDepartment = new SelectList(DepartLst);

            //string searchString =id;
            var doctors = from d in objDoc.getDoctors()
                          select d;
            //the above lines are LINQ query to select the doctors, created by the IndexMethod
            //The query is defined at this point, but hasn't been run against the data store.


            //If the searchString parameter contains a string, the doctors query is modified to filter on the value of the search string, using the following code.

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.lname.Contains(searchString));
            }

            //The following code shows how to check the medicalDepartment parameter. If it's not empty, the code further contains the doctors query to limit the selected doctors to the specified medical department.
            if (string.IsNullOrEmpty(medicalDepartment))
                return View(doctors);
            else
            {
                return View(doctors.Where(x => x.department == medicalDepartment));
            }
        }
    }
}


//[Team2]Temiskaming-Hospital website Dedesign Project @ Humber college
//Feature: Find a doctor
//Author: Jeesoo Kim
// Feb 17, 2015