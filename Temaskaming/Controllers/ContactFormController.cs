using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class ContactFormController : Controller
    {
        ContactFormClass objCF = new ContactFormClass();



        //start of index(public)
        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult index(ContactForm Cf)
        {
            ViewBag.Group = "ContactUs";

            if (ModelState.IsValid)
            {
                try
                {
                    objCF.CommitInsert(Cf);
                    return RedirectToAction("index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        //end of index (public )
   
   //admin side     
        [Authorize(Roles = "Admin")]
        public ActionResult contactformAdmin()
        {
            var Cf = objCF.getCF();
            ViewBag.Group = "Admin";
            return View(Cf);
        }


        //show Detail of each one
        [Authorize(Roles = "Admin")]
        public ActionResult contactformDetail(int id)
        {
            ViewBag.Group = "Admin";
            var Cf = objCF.getCFById(id);
            if (Cf == null)
            {
                return View("contactformAdmin");
            }
            else
            {
                return View(Cf);
            }
        }

        //Delete
        [Authorize(Roles = "Admin")]
        public ActionResult contactformDelete(int id)
        {
            ViewBag.Group = "Admin";
            var Cf = objCF.getCFById(id);
            if (Cf == null)
            {
                return View("contactformAdmin");
            }
            else
            {
                return View(Cf);
            }
        }
        
       //Delete
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult contactformDelete(int id, ContactForm Cf)
        {
            ViewBag.Group = "Admin";
            try
            {
                objCF.CommitDelete(id);
                return RedirectToAction("contactformAdmin");
            }
            catch
            {
                return View();
            }
        }

    }
}
