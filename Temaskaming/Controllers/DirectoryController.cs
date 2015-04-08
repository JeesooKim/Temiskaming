using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class DirectoryController : Controller
    {
        //Controller to manage the hospital directory (departments and staff)
        
        directoryClass objDir = new directoryClass();
        //initiating a directoryClass object (name: objDir), which is from a linq object in the model

        //*************** Public **************//
        public ActionResult Index()
        {
            ViewBag.Group="ContactUs";

            //Linq query to selet the departments
            var departments = from d in objDir.getDepartments()
                              select d;           
            return View(departments);
        }

        public ActionResult _staffIndex(int departmentId)
        {
            ViewBag.Group = "ContactUs";

            var staffs = objDir.getStaffByDepartmentID(departmentId);
            
            //var staffs = from s in objDir.getStaff()
            //             where s.staff_departmentId == departmentId
            //             select s;
            //var department = from t in objDir.getStaff()
            //                 where t.staff_departmentId==departmentId
            //                 select t.staff_departmentName.Distinct();

            if (staffs == null)
            {
                return PartialView("NotFound");
            }
            else
            {
                //ViewData["department"] = department;
                return PartialView(staffs);
            }
        }
        

        //************ Below, action methods for Amind-side ************//
        //Admin Index

        public ActionResult DirectoryAdmin()
        {
            //index page in the admin page for Directory
            //shows the list of departments with options to update and delete

            ViewBag.Group="Admin";

            var departments = from d in objDir.getDepartments()
                              select d;
            return View(departments);
        }

        public ActionResult DirectoryAdmin_Staff()
        {
            //Staff index page in the admin page for Directory
            //shows the list of Staff with options to update and delete

            ViewBag.Group="Admin";

            var staffs = from s in objDir.getStaff()
                              select s;
            return View(staffs);
        }

        public ActionResult Admin_departmentDetails(int id)
        {//parameter id: department ID
            
            ViewBag.Group="Admin";

            var department = objDir.getDepartmentByID(id);

            if (department == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(department);
            }
        }

        public ActionResult Admin_staffDetails(int id)
        {//parameter id: department ID (foreign Key in staffs table)
            
            ViewBag.Group="Admin";
            
            var staff = objDir.getStaffByID(id);

            if (staff == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(staff);
            }
        }

        //******** Department IUD *******//

        public ActionResult Admin_departmentInsert()
        {
            ViewBag.Group="Admin";
            return View();
        }

        [HttpPost]
        public ActionResult Admin_departmentInsert(department d)
        {
            ViewBag.Group="Admin";

            
            if(ModelState.IsValid)
            {
                try
                {
                    objDir.commitInsertD(d);
                    return RedirectToAction("DirectoryAdmin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Admin_departmentUpdate(int id)
        {
            ViewBag.Group="Admin";
            var d = objDir.getDepartmentByID(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(d);
            }
        }

        [HttpPost]
        public ActionResult Admin_departmentUpdate(int id, department d)
        {
            ViewBag.Group="Admin";
            if(ModelState.IsValid)
            {
                try
                {
                    objDir.commitUpdateD(id, d.d_name, d.d_phone, d.d_ext, d.d_fax);
                    return RedirectToAction("Admin_departmentDetails/" + id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        public ActionResult Admin_departmentDelete(int id)
        {
            ViewBag.Group="Admin";
            var d = objDir.getDepartmentByID(id);
            if(d==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(d);
            }
        }

        [HttpPost]
        public ActionResult Admin_departmentDelete(int id, department d)
        {
            ViewBag.Group = "Admin";
            try
            {
                objDir.commitDeleteD(id);
                return RedirectToAction("DirectoryAdmin");
            }
            catch
            {
                return View();
            }
        }

        //******** Staff IUD *******//

        public ActionResult Admin_staffInsert()
        {
            ViewBag.Group="Admin";
            return View();
        }

        [HttpPost]
        public ActionResult Admin_staffInsert(staff s)
        {
            ViewBag.Group="Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    
                    objDir.commitInsertS(s);
                    //int i= s.staff_id;
                    //int j = s.staff_departmentId;
                    //////****//
                    //////int i = (Int32)objDir.getStaffId(s).First();
                    //////int j = (Int32)objDir.getDepartmentId(s).First();
                    ////int i = (Int32)objDir.getStaff().Select(x => x.staff_id).First();                    
                    ////int j = (Int32)objDir.getStaff().Select(x => x.staff_departmentId).First();
                    //objDir.commitInsertDNameS(i,j);
                    //****//
                    return RedirectToAction("DirectoryAdmin_Staff");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Admin_staffUpdate(int id)
        {
            ViewBag.Group="Admin";

            var s = objDir.getStaffByID(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(s);
            }
        }

        [HttpPost]
        public ActionResult Admin_staffUpdate(int id, staff s)
        {
            ViewBag.Group="Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objDir.commitUpdateS(id, s.staff_fname, s.staff_lname, s.staff_position, s.staff_phone, s.staff_ext, s.staff_email, s.staff_departmentName, s.staff_departmentId);
                    return RedirectToAction("Admin_staffDetails/" + id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        public ActionResult Admin_staffDelete(int id)
        {
            ViewBag.Group="Admin";
            var s = objDir.getStaffByID(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(s);
            }
        }

        [HttpPost]
        public ActionResult Admin_staffDelete(int id, staff s)
        {
            ViewBag.Group = "Admin";
            try
            {
                objDir.commitDeleteS(id);
                return RedirectToAction("DirectoryAdmin_Staff"); //after the staff(id), return to the staff list
            }
            catch
            {
                return View();
            }
        }
        // ****** end of methods ****//        
    }
}

//[Team2]Temiskaming-Hospital website Redesign Project @ Humber College
//Feature: Directory
//File: directoryController.cs
//Author: Jeesoo Kim
//March30, 2015s


