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


        public ActionResult ContactForm()
        {
            var CF = objCF.getCF();
            ViewBag.Group = "Contact Form";
            return View();
        }

        public ActionResult CFAdmin()
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
        public ActionResult CFInsert(){
            return View();
        }


        [HttpPost]
        public ActionResult CFInsert(ContactForm Cf)
        {
            ViewBag.Group ="Contact Form";
            if(ModelState.IsValid)
            {
                try
                {
                    objCF.commitInsert(Cf);
                    return RedirectToAction("ContactForm");
                }
                catch 
                {
                    return View();
                }
            }
            return View();
        }

            public ActionResult CFDelete(int id)
            {
                var Cf = objCF.getCFByID(id);
                if (Cf == null)
                {
                    return View ("Not Found");
                }
                else{
                    return View(Cf);
                }
            }
        [HttpPost]
        public ActionResult CFDelete(int id, ContactForm Cf)
        {
            ViewBag.Group ="Contact Form";
            try
            {
                objCF.commitDelete(id);
                return RedirectToAction("CFAdmin");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult NotFound ()
        {
            return View();
        }

   }

 }

  

