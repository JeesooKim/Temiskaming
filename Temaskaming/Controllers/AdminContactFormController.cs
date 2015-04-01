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

        public ActionResult CFDelete()
        {
            return View();
        }
    }
}
