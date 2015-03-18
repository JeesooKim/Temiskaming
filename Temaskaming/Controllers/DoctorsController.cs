using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;
using PagedList;

namespace FindADoctor.Controllers
{
    public class DoctorsController : Controller
    {
        //
        // GET: /Doctors/
        doctorClass objDoc = new doctorClass();
        //initiating a doctorClass object, objDoc, which is from a linq object in the model



        //Public Index->SEARCH FOR DOCTORS (FIND A DOCTOR)
        //Ref: http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
        //http://forums.asp.net/t/2026344.aspx?How+to+keep+the+search+term+in+textbox+after+postback+when+implemented+with+jquery+Watermark+MVC4+Razorview

        public ActionResult Index(string sortOrder, string currentFilter, string medicalDepartment, string searchString, int? page)
        {
            ViewBag.Group = "ContactUs";
            ViewBag.currentSort = sortOrder;
            //"ViewBag.CurrentSort: keep the current sortorder the same while paging"

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DepartSortParm = sortOrder == "Department" ? "departmt_desc" : "Department";


            ViewBag.currentFitler = searchString;
            //"ViewBag.currentFilter : to maintain the filter setting during paging, with the textbox restoring the searchString Value when the page is redisplayed"

            //string searchString =id;
            var doctors = from d in objDoc.getDoctors()
                          select d;
            //the above lines are LINQ query to select the doctors, created by the IndexMethod
            //The query is defined at this point, but hasn't been run against the data store.

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            var DepartLst = new List<string>();
            //this creates a List object to hold medical departments from the database.
            //the following code is a LINQ query that retrieves all the medical departments from the db.
            var DepartQry = from m in objDoc.getDoctors()
                            orderby m.department
                            select m.department;
            DepartLst.AddRange(DepartQry.Distinct());
            //AddRange: the method of the generic List collection to add all the distinct medical departments to the list. Without the Distinct modifier, duplicate departments would be added.

            ViewBag.medicalDepartment = new SelectList(DepartLst);
            
            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(s => s.lname);
                    break;
                case "Department":
                    doctors = doctors.OrderBy(s => s.department);
                    break;
                case "departmt_desc":
                    doctors = doctors.OrderByDescending(s => s.department);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.lname);
                    break;
            }

            //If the searchString parameter contains a string, the doctors query is modified to filter on the value of the search string, using the following code.

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.lname.Contains(searchString));
            }

            //The following code shows how to check the medicalDepartment parameter. If it's not empty, the code further contains the doctors query to limit the selected doctors to the specified medical department.
            if (!string.IsNullOrEmpty(medicalDepartment))
            {
                doctors= doctors.Where(x => x.department == medicalDepartment);
            }
            return View(doctors);
        }// End of Index
        
        
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
                return View(doc);   //when get a doc's info having id, return it to Details page(View)
            }
        }

        //*************** Below, action methods are only for Admin-side *****************//
        // Admin index page will be different and added below.
        // Insert, Update, and Delete
       
        public ActionResult FindADocAdmin(string sortOrder, string currentFilter, string medicalDepartment, string searchString, int? page)
        {//Admin Index page:this action method is basically the same as the public index page 
            //but three more options added, update and delete for each row (doctor), and insert
            ViewBag.Group = "Admin";
            ViewBag.currentSort = sortOrder;
            //"ViewBag.CurrentSort: keep the current sortorder the same while paging"

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DepartSortParm = sortOrder == "Department" ? "departmt_desc" : "Department";


            ViewBag.currentFitler = searchString;
            //"ViewBag.currentFilter : to maintain the filter setting during paging, with the textbox restoring the searchString Value when the page is redisplayed"

            //string searchString =id;
            var doctors = from d in objDoc.getDoctors()
                          select d;
            //the above lines are LINQ query to select the doctors, created by the IndexMethod
            //The query is defined at this point, but hasn't been run against the data store.

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            var DepartLst = new List<string>();
            //this creates a List object to hold medical departments from the database.
            //the following code is a LINQ query that retrieves all the medical departments from the db.
            var DepartQry = from m in objDoc.getDoctors()
                            orderby m.department
                            select m.department;
            DepartLst.AddRange(DepartQry.Distinct());
            //AddRange: the method of the generic List collection to add all the distinct medical departments to the list. Without the Distinct modifier, duplicate departments would be added.

            ViewBag.medicalDepartment = new SelectList(DepartLst);


            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(s => s.lname);
                    break;
                case "Department":
                    doctors = doctors.OrderBy(s => s.department);
                    break;
                case "departmt_desc":
                    doctors = doctors.OrderByDescending(s => s.department);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.lname);
                    break;
            }

            //If the searchString parameter contains a string, the doctors query is modified to filter on the value of the search string, using the following code.

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.lname.Contains(searchString));
            }

            //The following code shows how to check the medicalDepartment parameter. If it's not empty, the code further contains the doctors query to limit the selected doctors to the specified medical department.
            if (!string.IsNullOrEmpty(medicalDepartment))
            {
                doctors = doctors.Where(x => x.department == medicalDepartment);
            }            
            return View(doctors);
        }

        public ActionResult FindADocDetails(int id)
        {
            
            var doc = objDoc.getDoctorByID(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(doc);   //when get a doc's info having id, return it to Details page(View)
            }
        }

        public ActionResult FindADocInsert()
        {//when firstly Insert page is loaded...this action method is for Admin
            ViewBag.Group = "Admin";
            return View();  
        }


        [HttpPost]
        public ActionResult FindADocInsert(doctor doc)
        {//when Insert page is working with doc...this action method is for Admin
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {//when model is valid, try to insert the parameter info into db thru the objDoc object 
                //Then, return to Index page.
                try
                {
                    objDoc.commitInsert(doc);
                    return RedirectToAction("Index");
                }
                catch
                {//exception handling...return to Insert page
                    return View();
                }

            }

            return View();
        }

        public ActionResult FindADocUpdate(int id)
        {
            ViewBag.Group = "Admin";
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

        [HttpPost]
        public ActionResult FindADocUpdate(int id, doctor doc)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objDoc.commitUpdate(id, doc.fname, doc.lname, doc.title, doc.department, doc.role, doc.program, doc.status, doc.email, doc.extension, doc.phone, doc.office, doc.office_hr, doc.bio);
                    return RedirectToAction("Details/" + id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        public ActionResult FindADocDelete(int id)
        {
            ViewBag.Group = "Admin";
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

        [HttpPost]
        public ActionResult FindADocDelete(int id, doctor doc)
        {
            ViewBag.Group = "Admin";
            try
            {
                objDoc.commitDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //[Team2]Temiskaming-Hospital website Dedesign Project @ Humber college
        //Feature: Find a doctor
        //Author: Jeesoo Kim
        // Feb 17, 2015

        //Sorting/Paginagion for the Index action method : referenced and updated on March17, 2015
        }
}