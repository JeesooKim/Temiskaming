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




        public ActionResult ContactForm(ContactForm Cf)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactForm(ContactForm Cf)
        {
            ViewBag.Group = "Contact Form";

            if (ModelState.IsValid)
            {
                try
                {
                    objCF.CFInsert(Cf);
                    return RedirectToAction("ContactForm");
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
