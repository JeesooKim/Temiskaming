using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;


namespace Temiskaming.Controllers
{
    public class AdminContactFormController : Controller
    {

        ContactFormClass objCF = new ContactFormClass();


        public ActionResult ContactFormAdmin()
        {
            var Cf = objCF.getCF();
            return View(Cf);
        }


        //show Detail of each one
        public ActionResult ContactFormDetail()
        {
            ViewBag.Group = "Contact Form";
            var Cf = objCF.getCF();
            if (Cf == null)
            {
                return View("CFAdmin");
            }
            else
            {
                return View(Cf);
            }
        }

        //Delete
        public ActionResult ContactFormDelete(int id)
        {
            var Cf = objCF.getCFById(id);
            if (Cf == null)
            {
                return View("CFAdmin");
            }
            else
            {
                return View(Cf);
            }
        }
        
       //Delete
        [HttpPost]
        public ActionResult ContactFormDelete(int id, ContactForm Cf)
        {
            ViewBag.Group = "Contact Form";
            try
            {
                objCF.CommitDelete(id);
                return RedirectToAction("CFAdmin");
            }
            catch
            {
                return View();
            }
        }

    }
}
