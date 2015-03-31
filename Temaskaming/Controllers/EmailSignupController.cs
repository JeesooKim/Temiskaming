using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

namespace VeronikaProject.Controllers
{
    public class EmailSignupController : Controller
    {
        linqemailsClass objSignup = new linqemailsClass();

        public ActionResult EmailSignup()
        {
            return View();
        }

        public ActionResult SignupComplete()
        {
            return RedirectToAction("EmailSignup");
        }

        [HttpPost]
        public ActionResult SignupComplete(email_signup valid)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objSignup.commitInsert(valid);
                    return PartialView(valid);
                }
                catch
                {
                    return View("EmailSignup");
                }
            }
            else
            {
                return View("EmailSignup");
            }
        }

    }
}
