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
            ViewBag.Group = "Contact Form";

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

        public ActionResult contactformAdmin()
        {
            var Cf = objCF.getCF();
            ViewBag.Group = "contactformAdmin";
            return View(Cf);
        }


        //show Detail of each one
        public ActionResult contactformDetail(int id)
        {
            ViewBag.Group = "Contact Form";
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
        public ActionResult contactformDelete(int id)
        {
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
        [HttpPost]
        public ActionResult contactformDelete(int id, ContactForm Cf)
        {
            ViewBag.Group = "Contact Form";
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
