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



        [HttpPost]
        public ActionResult ContactForm(ContactForm Cf)
        {
            var CF = objCF.getCF(); 
            ViewBag.Group="Contact Form";
            return View();
        }

        public ActionResult CFAdmin()
        {
            ViewBag.Group="Contact Form";
            var Cf= objCF.getCF();
            if (Cf== null)
            {
                return View ("CFAdmin");
            }
            else
            {
                return View(Cf);
            }
        }

        public ActionResult CFDelete()
        {

        }
    }
}
