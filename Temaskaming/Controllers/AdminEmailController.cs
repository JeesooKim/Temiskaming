using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;
namespace Temiskaming.Controllers
{
    // controller which inserts, updates, and deletes data email signup table

    public class AdminEmailController : Controller
    {
        linqemailsClass objSignup = new linqemailsClass();

        //action result which shows all email signups
        public ActionResult AdminShowSignups()
        {
            ViewBag.Group = "Admin";
            var signups = objSignup.getSignups();
            return View(signups);
        }
        // action method which deletes email signup based on id 
        public ActionResult Delete(int id)
        {
            ViewBag.Group = "Admin";
            var card = objSignup.getSignupsbyID(id);
            if (card == null)
            {
                return View("NotFound");
            }
            else
            {
                objSignup.commitDelete(id);
                return RedirectToAction("AdminShowSignups");
            }
        }
        // action method which shows an email signup to be updated
        public ActionResult Update(int id)
        {
            ViewBag.Group = "Admin";
            var email = objSignup.getSignupsbyID(id);
            if (email == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(email);
            }
        }
        // action result of post method which updates email signup if the data is valid
        [HttpPost]
        public ActionResult Update(int id, email_signup email)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objSignup.commitUpdate(id, email.ename, email.elname, email.eemail);
                    return RedirectToAction("AdminShowSignups");
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
